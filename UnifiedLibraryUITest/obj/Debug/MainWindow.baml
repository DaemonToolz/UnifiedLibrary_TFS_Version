   M S B A M L   `   `   `  ����O  KUnifiedLibraryUITest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UnifiedLibraryUITestUnifiedLibraryUITest  5   	   %    UnifiedLibraryUITest.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:UnifiedLibraryUITest     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null4wpfanimated"http://wpfanimatedgif.codeplex.com  5   	   -   5          �Name$
  Root��5   	    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ UI Test��5   	   $	��400��6   $	��600��6&   .��5      � -   5         �Name$ MainGrid��5      
���   � � Cursor$
 Hand��60    � RowDefinitions 5   
   �� 5      $��0* �6   �� 5      .+�5   
   � -   5        $ MainActionGrid��5      $
��Left=�6'   $��45��6B   $��530,330,0,0q�6N   $	��Top=�6c   $��45��6{   $��2��6}  ��5      �� 5       �� ImageSource$2 ,pack://application:,,,/Images/power-logo.png��6    j� Stretch$ Uniform=�6X   5      � 5   
   -   5        $ PrevActionGrid��5      $
��Left=�6'   $	��105��6B   $��530,120,0,0q�6O   $	��Top=�6d   $��60��6|   $��2��6�   ��5      �� 5      $, &pack://application:,,,/Images/next.png��6   $ Uniform=�6R   5      � 5   
   -   5        $ NextActionGrid��5      $
��Left=�6'   $	��105��6B   $��
10,120,0,0q�6O   $	��Top=�6c   $��60��6{   $��2��6�   ��5      �� 5      $, &pack://application:,,,/Images/back.png��6   $ Uniform=�6R   5       � 5!   
   -   5        $ 	MainFrame��5!      $
��Left=�6"   $	��250��6=   $��	70,65,0,0q�6J   $	��Top=�6]   $	��460��6u   $��2��6�   .+�5"      � -   5        $ ActionAddAcountGrid��5"      $��Center=�60   $	��230��6M   $��10q�6Z   $��Center=�6f   $	��440��6�   ��5#      �� 5$      $ Uniform=�6!   $+ %pack://application:,,,/Images/add.png��63   5&      5'      �� 5(   
   -   5        	 ��Name$	 Header��5(      
���   �$
��Left=�61   $��45��6L   $��2��6X   
���   �$	��Top=�6x   $	��600��6�   5*      5+      