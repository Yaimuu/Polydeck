#include <base64.hpp>

/*
 *  This sketch sends data via HTTP GET requests to data.sparkfun.com service.
 *
 *  You need to get streamId and privateKey at data.sparkfun.com and paste them
 *  below. Or just customize this script to talk to other HTTP servers.
 *
 */
#include <HTTPClient.h>
#include <WiFi.h>
#include <WiFiClientSecure.h>
#include <ssl_client.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SH110X.h>


#define SCREEN_WIDTH 64  // OLED display width, in pixels
#define SCREEN_HEIGHT 128 // OLED display height, in pixels
Adafruit_SH1107 display = Adafruit_SH1107(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire);

#define BUTTON 39

// OLED FeatherWing buttons map to different pins depending on board:
#define BUTTON_A 15
#define BUTTON_B 32
#define BUTTON_C 14

typedef struct {
  String code;
  String payload;
} Response;

typedef struct {
  int last = 0;
  int current = 0;
  int max = 0;
} ButtonManager;

typedef struct {
  int id = 0;
  int realButton = 0;
} VirtualButton;
int maxImageSize = 64*64;
static const unsigned char PROGMEM *logo_bmp;

const char* ssid     = "DESKTOP-TUKKEJD 4851";
const char* password = "80yW^854";

const char* host = "192.168.137.1";
const int httpPort = 5083;
const int httpsPort = 7224;
const char* streamId   = "....................";
const char* privateKey = "....................";

int currentDisplay = 0;
int lastDisplay = 0;

ButtonManager managerB;
ButtonManager managerC; 

// Use WiFiClient class to create TCP connections
WiFiClient client;
WiFiClientSecure clientSecure;
HTTPClient httpClient;

void setup()
{
    pinMode(BUTTON, INPUT);

    pinMode(BUTTON_A, INPUT_PULLUP);
    pinMode(BUTTON_B, INPUT_PULLUP);
    pinMode(BUTTON_C, INPUT_PULLUP);

    Serial.begin(115200);
    delay(250);
    
    display.begin(0x3C, true); // Address 0x3C default
    Serial.println("OLED begun");

    // Show image buffer on the display hardware.
    // Since the buffer is intialized with an Adafruit splashscreen
    // internally, this will display the splashscreen.
    display.display();
    delay(1000);
    // Clear the buffer.
    display.clearDisplay();
    display.display();
    display.setRotation(1);

    // We start by connecting to a WiFi network
    Serial.println();
    Serial.println();
    Serial.print("Connecting to ");
    Serial.println(ssid);

    display.setTextSize(1);
    display.setTextColor(SH110X_WHITE);
    display.setCursor(0,0);
    display.print("Connecting to ");
    display.print(ssid);
    display.display();

    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
        display.print(".");
        display.display();
    }

    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
    Serial.println(WiFi.macAddress());

    Serial.println("LCD setup");

    // text display tests
    display.println("");
    display.println("WiFi connected");
    display.print("IP address: ");
    display.println(WiFi.localIP());
    display.println(WiFi.macAddress());
    display.display(); // actually display all of the above
    
    currentDisplay = 0;
    managerB.current = 0;
    managerB.max = 2;
    managerC.current = 0;
    managerC.max = 2;

    // httpClient.setTimeout(5);
}

void loop()
{
  if(!digitalRead(BUTTON_A))
  {
    Serial.println("Button A");    
  }

  if(!digitalRead(BUTTON_B) && managerB.last == managerB.current)
  {
    Serial.println("Button B");

    
    
    if(managerC.current)
    {
      // Clear the buffer.
      display.clearDisplay();
      display.display();
      display.setRotation(1);
      displayLogo(managerB.current);      
    }

    managerB.current = (managerB.current + 1) % managerB.max;
    Serial.println(managerB.current);
  }
  else if(digitalRead(BUTTON_B)==HIGH)
  {
    managerB.last = managerB.current;
  }
  
  if(!digitalRead(BUTTON_C) && managerC.last == managerC.current)
  {
    Serial.println("Button C");
    // Clear the buffer.
    display.begin(0x3C, true);   
    display.display();
    delay(1000); 
    display.clearDisplay();
    display.display();
    display.setRotation(1);

    managerC.current = (managerC.current + 1) % managerC.max;
    
    Serial.println("Display " + String(managerC.current));

    switch(managerC.current)
    {
      case 0:
        displayWifiInfos();
        break;
      case 1:
        displayLogo(managerB.current);
        break;
    }
  }
  else if(digitalRead(BUTTON_C)==HIGH)
  {
    managerC.last = managerC.current;
  }
  
  if (digitalRead(BUTTON)==LOW) {
    
    if ((WiFi.status() == WL_CONNECTED))
    {
      String url = "http://";
      url += host;
      url += ":";
      url += httpPort;
      url += "/device/";
      url += WiFi.macAddress();

      Response getDevice = getAPI(url);
      
      if(getDevice.code == "404") {
        initialize();
      }

      executeButton(managerB.current);
    }
  }
  
}

void initialize()
{
  String url = "http://";
  url += host;
  url += ":";
  url += httpPort;
  url += "/device/init";

  String payload = "{\"name\":\"Test Device\",\"macAddress\":\"" + WiFi.macAddress() + "\",\"deviceGPIOs\":[{\"pin\":0,\"action\":{\"shortcuts\":[123]}, \"logoPath\":\"\"}]}";

  
  postAPI(url, payload);
}

void executeButton(int button)
{
  String url = "http://";
  url += host;
  url += ":";
  url += httpPort;
  url += "/device/action/";
  url += WiFi.macAddress();
  url += "/" + String(button);
  
  postAPI(url, "");
}

Response getAPI(String url)
{
  Response response;
  response.code = "";
  response.payload = "";
  
  Serial.print("Requesting URL: ");
  Serial.println(url);
  
  httpClient.begin(url); //Specify the URL
  int httpCode = httpClient.GET(); //Make the request

  if (httpCode > 0) { //Check for the returning code

    String payload = httpClient.getString();
    Serial.println(httpCode);
    Serial.println(payload);

    response.code = httpCode;
    response.payload = payload;    
  }
  else {
    Serial.println("Error on HTTP request");
  }

  httpClient.end();
  delay(1000);
  Serial.println("Connection closed");

  return response;
}

Response postAPI(String url, String payload)
{
  Response response;
  response.code = "";
  response.payload = "";
  
  Serial.print("Requesting URL: ");
  Serial.println(url);
  
  httpClient.begin(url); //Specify the URL

  httpClient.addHeader("Content-Type", "application/json");
  httpClient.addHeader("Content-Length", String(payload.length()));
  httpClient.addHeader("Connection", "keep-alive");
  httpClient.addHeader("Accept", "*/*");
  int httpCode = httpClient.POST(payload); //Make the request
  Serial.println(payload);
  if (httpCode > 0) { //Check for the returning code

    String responsePayload = httpClient.getString();
    Serial.println(httpCode);
    Serial.println(responsePayload);

    response.code = httpCode;
    response.payload = payload;  
  }
  else {
    Serial.println("Error on HTTP request");
  }

  httpClient.end();
  delay(1000);
  Serial.println("Connection closed");

  return response;
}

void displayWifiInfos()
{
    if ((WiFi.status() == WL_CONNECTED))
    {
      display.setTextSize(1);
      display.setTextColor(SH110X_WHITE);
      display.setCursor(0,0);
      display.print("Connected to ");
      display.print(ssid);
      display.display();    

      Serial.println("");
      Serial.println("WiFi connected");
      Serial.println("IP address: ");
      Serial.println(WiFi.localIP());
      Serial.println(WiFi.macAddress());

      // text display tests
      display.println("");
      display.println("WiFi connected");
      display.print("IP address: ");
      display.println(WiFi.localIP());
      display.println(WiFi.macAddress());
      display.display();  
    }
    else
    {
      display.println("No WiFi connection found for : " + String(ssid));
      display.display();        
    }
}

void displayLogo(int button)
{
  String url = "http://";
  url += host;
  url += ":";
  url += httpPort;
  url += "/device/logo/";
  url += WiFi.macAddress();
  url += "/" + String(button);
  
  Response logoResponse = getAPI(url);
  
  Serial.println("Create buffer");  
  String payload = logoResponse.payload/*.substring(55)*/;
  payload.replace("\"", "");
  // payload.replace(" ", "");
  // payload.replace("-", "");
  // payload.replace(",", "");
  // payload.replace("\n", "");
  // payload.replace("0x", "");

  Serial.println("Display payload");
  // Serial.println(payload);

  
  // int bufSize = (int)((float)payload.length() * 0.66f) + 1;
  // int bufSize = payload.length();
  // byte buff[bufSize / 2];
  
  // Serial.println("Decode payload");  
  // unsigned char *encoded = reinterpret_cast<unsigned char *>( &payload[0] );
  // unsigned char *decoded;
  // Serial.println("Test");
  // Serial.println((char *)encoded); 
  // unsigned int string_length = decode_base64(encoded, decoded);

  // Serial.println(String(decoded));
  
  // Serial.println(bitmap);
  // uint8_t bitmap2[payload.length()];
  // payload.getBytes(bitmap2, payload.length());

  Serial.println("Payload to buffer");
  char *bitmap = &payload[0];  

  // for (int i = 0; i < payload.length(); i += 2) {
  //   String byteString = payload.substring(i, i + 2);
  //   buff[i / 2] = (byte) strtol(byteString.c_str(), NULL, 16);
  // }

  // memcpy_P (buff, logo_bmp, sizeof(buff));
  Serial.println("Logo display"); 
  // display.drawBitmap(
  //   (display.width()  - 64 ) / 2,
  //   (display.height() - 64) / 2,
  //   (uint8_t *)bitmap, SCREEN_WIDTH, SCREEN_HEIGHT, 1);
  display.setCursor(0,0);
  display.println("Bouton virtuel - " + String(button));  
  
  // display.drawRamBitmap(
  //   (display.width()  - 64 ) / 2,
  //   (display.height() - 64) / 2,
  //   64, 64,1, reinterpret_cast<const unsigned char*>(payload.c_str()), 128*64);
  
  display.display();
}
