   M S B A M L   `   `   `  ����C  ?PolyDeck, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PolyDeckPolyDeck  5   	       PolyDeck.AddGPIO   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PolyDeck     5   	     � Title$  Add GPIO��5   	   $	��210��6   $	��210��6'   $	��210��67   $	��460��6G   $	��460��6S   $	��460��6b   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   �� $��0��6   $��Select a device��6   $��
20,10,0,10q�67   $��24(�6K   $��Segoe UI Semibold)�6Y   �� 5   
   -   5         ��Name$ ComboBox��5      $	��400��6%   $��1��61    �� SelectedIndex$ 0��5      $��30��6    �� DisplayMemberPath$
 Name��5      $��20,10,20,10q�6&   ��5      �� 5   &   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��
0,10,20,10q�6W   .+�5      �� -   5         ��Name$ btn_Ok��5      $��Ok��6%   $��20��62   $��70��6>   $��Right=�6I   $��0,0,10,0q�6e   6w   �� 5      -   5        $ 
btn_Cancel��5      $��Cancel��6)   $��20��6:   $��70��6F   $��Right=�6Q   6m   5      5      5      