   M S B A M L   `   `   `  ����A  =UL_UIP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UL_UIPUL_UIP  5          UL_UIP.SettingsPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:UL_UIP     5        N� Title$  GenericAccountPage��5	      .��5      � -   5         �Name$ MainGrid��5      .+�5   
   �� -   5         ��Name$ 
TitleLabel��5      
�������$��Settings��67   $
��Left=�6J   $	��Top=�6e   $
��Bold"�6}   $��14(�6�   $	��600��6�   $��36��6�   $��0,0,0,2q�6�   
�������6�   �� 5   
   -   5        $ Setting_Name��5      $��This setting��6&   $
��Left=�6=   $��	32,39,0,0q�6X   $	��Top=�6k   
�������$
��Bold"�6�   �� 5   
   -   5        $ Setting_Name_Copy��5      $��This setting��6+   $
��Left=�6B   $��	32,70,0,0q�6]   $	��Top=�6p   
�������$
��Bold"�6�   �� 5   
   -   5        $ Setting_Name_Copy1��5      $��This setting��6,   $
��Left=�6C   $��
32,132,0,0q�6^   $	��Top=�6r   
�������$
��Bold"�6�   �� 5   
   -   5        $ Setting_Name_Copy2��5      $��This setting��6,   $
��Left=�6C   $��
32,101,0,0q�6^   $	��Top=�6r   
�������$
��Bold"�6�   �� 5   
   $��Center=�6      CheckBoxStyle1#���   60   $��Center=�6Y   $��151,39,394,247q�6t   �� 5   
   $��Center=�6   #���   60   $��Center=�6Y   $��125,72,368,215q�6t   �� 5   
   $��Center=�6   #���   60   $��Center=�6Y   $��125,101,368,185q�6t   �� 5   
   -   5        $��Center=�5      #���   60   $��Center=�6Y   $��125,132,368,155q�6t   6�   �� 5   
   -   5        $ ThemeSettingsLabel��5      $��Theme��6,   $
��Left=�6<   $��
32,276,0,0q�6W   $	��Top=�6k   
�������$
��Bold"�6�   �� 5   
   -	   5         ��Name$ ThemeSelector��5      $
��Left=�6*   $��151,276,0,0q�6E   $	��Top=�6Z   $	��120��6r   6~   5       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType	 � 	Resources  Key
 g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value p� KeyTime p� Value ]�Name J� SnapsToDevicePixels �� ContentStringFormat �� ContentStringFormat ��Name �� CornerRadius J� RenderTransformOrigin �� ScaleX �� ScaleY �� AngleX �� AngleY �� Angle T� X T� Y �� EndPoint  �� 
StartPoint! � Color" � Offset# �� Triggers$ P� Property% P� Value& N� ExitActions' ��Name  OnUnchecking( N� EnterActions  
OnChecking��%�  &	        7  7  �� 5   
   " ��6'   .�5       ��  � ��# � ��6+   �� 5!       � ��# � ��6+   �� 5"       � �� 5#      �� 5$      " ��6&   	 5%      ���� 5&      
OnChecking .��5'   "   U� $
 00:00:00X�6@   $ slider��6U   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6t   .\�5(   &   �� $ 00:00:00.1000000��6;   $ 25Q�6V   5)   #   5*      �� 5+      OnUnchecking .��5,   "   U� $
 00:00:00X�6@   $ slider��6U   $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6t   .\�5-   &   �� $ 00:00:00.1000000��6;   $ 0Q�6V   5/   #   r� 50   "   $
 00:00:00X�6C   $ slider��6X   $ (FrameworkElement.Margin)�6w   .	�51   &   �� $ 00:00:00.1000000��6>   $ 1,1,1,1q�6Y   52   #   53      54      .R�56      ]� $ 	dockPanel��6$   .+�57      �� # z 6/   #��z��6k   # z 6�   #��z��6�   ��. $��Center=�6  � 58      $��5,5,0,5q�6#   $��50��64   .+�59   "   �� $��ON��6,   $
��Wrap=�66   $
��Bold"�6J   $��12(�6\   
�������$��Right=�6}   $��0,0,3,0q�6�   �� 5:   "   $
��Left=�6,   $��2,0,0,0q�6G   $��12(�6X   
�������$
��Bold"�6y   $	��OFF��6�   $
��Wrap=�6�   �� 5;   "   $ slider��6D   $
��Left=�6)   $��23��6T   $��1,1,1,1q�6_   $ 3,3,3,3��6y   $ 0.5,0.5&�6�   $��1,1,1,1q�6�   z�5<   &   V� 5=   *   .~�5>   .   �� $ 1Q�6=   $ 1Q�6H   �� 5?   .   $ 0Q�6<   $ 0Q�6G   �� 5@   .   $ 0Q�6>   T� 5A   .   $ 0Q�6A   $ 0Q�6G   5B   +   ��5D   &   �� 5E   *   $	 0,1&�6>   $	  0,0&�6M   .��5F   .   � $! #00DD00��6;   $" 0Q�6K   � 5G   .   $! #00DD00��6;   $" 0Q�6K   5H   +   ��5J   &   �� 5K   *   $	 0,1&�6>   $	  0,0&�6M   .��5L   .   � $! #00DD00��6;   $" 0Q�6K   � 5M   .   $! #00DD00��6;   $" 0Q�6K   5N   +   5P   #   5Q      5R      # 5T      P� 5U      $ �@d�	IsChecked$
% TrueY�6;   & 5V   "   �� 5W   &   $"' OnUnchecking_BeginStoryboard��6a   8��  ( 5Y   "   �� 5Z   &   $ ' OnChecking_BeginStoryboard��6_   8�� 5\      P� 5]      $ � {�% .  . �5^   "   �� # � ��6?    � ��5_      5a      5c      5d      5e      5f      