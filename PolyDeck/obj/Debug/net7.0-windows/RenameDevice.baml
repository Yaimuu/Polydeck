   M S B A M L   `   `   `  ����C  ?PolyDeck, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PolyDeckPolyDeck  5   	       PolyDeck.RenameDevice   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   "localclr-namespace:PolyDeck  5   	     � Title$  Rename Device��5	   	   $	��240��6   $	��240��6,   $	��240��6<   $	��460��6L   $	��460��6X   $	��460��6g   .��5
      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   �� $��0��6   $��Rename Device��6   $��
20,10,0,10q�65   $��24(�6I   $��Segoe UI Semibold)�6W   �� 5   
   -   5         ��Name$ 	CB_Device��5      $	��400��6&   $��1��62    �� SelectedIndex$ 0��5      $��30��6    �� DisplayMemberPath$
 Name��5      $��20,10,20,10q�6&   ��5      �� 5   ;   �� 5   
   -   5         ��Name$ TXT_DeviceName��5      $��2��6*   $	��400��67   $��20,10,20,10q�6C   .��6X   5      �� 5   
   $��3��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��
0,10,20,10q�6W   .+�5      �� -   5         ��Name$ btn_Ok��5      $��Ok��6%   $��20��62   $��70��6>   $��Right=�6I   $��0,0,10,0q�6e   6w   �� 5      -   5        $ 
btn_Cancel��5      $��Cancel��6)   $��20��6:   $��70��6F   $��Right=�6Q   6m   5      5      5      