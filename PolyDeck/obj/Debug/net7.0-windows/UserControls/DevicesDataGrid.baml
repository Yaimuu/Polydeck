   M S B A M L   `   `   `  ����C  ?PolyDeck, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PolyDeck.UserControlsPolyDeck.UserControls  5      #clr-namespace:PolyDeckPolyDeck  5      H DPolyDeckModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:PolyDeckModel;assembly=PolyDeckModelPolyDeckModel 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
      +    %PolyDeck.UserControls.DevicesDataGrid   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:PolyDeck.UserControls     5      +polydeckclr-namespace:PolyDeck     5      Dmodel2clr-namespace:PolyDeckModel;assembly=PolyDeckModel     5      T	 PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f;controls,http://metro.mahapps.com/winfx/xaml/controls 	 5	      1core&clr-namespace:System;assembly=mscorlib 	 5
      $��20,20,20,20q�5   7     � 	Resources  5        PolyDeck.DevicesGPIOView   devicesgpioview  cvsDevicesGPIO �� Source �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription   PropertyName.   (PolyDeck.UserControls.GroupNameConverter  	converter��%�   &	        &	      7   &	 \       5   
   6#   �� 5   
   8    5        5      $ Device��6+   5        5   
   6#   5      ��5      � 5   
    � ColumnDefinitions 5      �� 5      $��* �6#   6/   �� 5      $	��200 �6#   61   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGrid��5         AutoGenerateColumns$ False��6h   $��0��6�      CanUserAddRows$ False��6�      SelectionMode$ Single=�6�   ��6)   �� 	 �� Source#	 [ 
 �� 
GroupStyle
 5       (  "System.Windows.Controls.GroupStyle  5"         ContainerStyle 5#      �� 5$       �� 
TargetType" ��6$   .�5%   "   ��  �� Property � �� �� Value$ 0,0,0,5q�6;   �� 5&   "    � �� 5'   &   �� 5(   *    �� 
TargetType" ��6:   .R�5)   .   9�  9� 
IsExpanded$
 True��67   
���   �$��1,1,1,5q�6\   ��5*   2   ]� 5+   6   .+�5,   :   �� $
��Bold"�6�   
�������$��5,0,0,0q�6�   $
��auto��6�   ��6D   ��  �� Path$
 Name� �� 	Converter# [ 6�   �� 5-   :   $
��Bold"�6D   
���������6j   �� $ 	ItemCount�5.   7   ��50   2   �� 51   6   53   /   54   +   56   #   57      59         Columns 5;      8 	 2MahApps.Metro.Controls.DataGridNumericUpDownColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$	 Pin��6;      Minimum$ 0Q�5>   +      CanUserSort$ False��67      MinWidth$	 100Q�6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5=   +   �� *Pin+5>   K   0	  *System.Windows.Controls.DataGridTextColumn	  5?      $ 	Logo path��6)   $ False��6Y   $	 200Q�6m    6<   �� *
LogoPath+6m   	  5@      -   5        DGCOL_Shortcuts��$ 	Shortcuts��5@   B   $ False��6y      
IsReadOnly$ False��6�   $	 200Q�6�    6U   �� *ShortcutsString+6�   5B      �� 5C      $��1��6   .+�5D      �� -   5         ��Name$ btn_AddGPIO��5D      $��Add GPIO��6.   $��
10,30,-1,0q�6A   6U   �� 5E      -   5        $ btn_RenameDevice��5E      $��Rename device��63   $��
10,10,-1,0q�6K   6_   �� 5F      -   5        $ BTN_Save��5F      $
��Save��6+   $��	10,10,0,0q�6:   6M   5G      �� 5H      $��1��6   5I      5K      