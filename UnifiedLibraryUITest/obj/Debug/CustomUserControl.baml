   M S B A M L   `   `   `  ����O  KUnifiedLibraryUITest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UnifiedLibraryUITest.UIUnifiedLibraryUITest.UI  5      ,    &UnifiedLibraryUITest.CustomUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9localui%clr-namespace:UnifiedLibraryUITest.UI     5        � 	Resources  5
      6   0UnifiedLibraryUITest.UI.ProgressToAngleConverter   ProgressConverter �� 
TargetType  ProgressBarStyle �� Property �� Value �� 
TargetType!   UnifiedLibraryUITest.UI.Arc   
StartAngle `� 	Converter �� Path �� RelativeSource	   EndAngle
 �� StringFormat��%_  &	        &	      7   7     5   
   6+   �� 5   
   " �6   .�5      ��  � �� 5      �� 5      " �6&   .R�5      � .+�5      A� 
���   �#��z��65   A� 5      
���   �$��40q�65   
�������6A     5      $��30��6*   #��z��6?   $��5q�6f    5   "   `� 5   &   8   .1�5   *   �� $ Minimum�62    6A   �� *TemplatedParent+�� 5   *   $ .�62    6;   �� *TemplatedParent+5   '   	 5   "   `� 5   &   8  .1�5   *   �� $ Value�62    6?   �� *TemplatedParent+�� 5   *   $ .�62    6;   �� *TemplatedParent+5   '   5       �� 5!      #��z��5"   (   $��Center=�6R   $��Center=�6m   $��72(�5#   (   $
��Bold"�66   ��5!   (   �� *Value+ �� *TemplatedParent+$
 {0:0}��5#   6   5$      5%      5'      5(      5)      .��5*      �  � RowDefinitions 5+   
   �� 5,      $��* �6   �� 5-      $
��auto �6   .+�5/   
   (� .��50      � #��[ 6   $	��300��6D   $	��300��6P    � Value 51      ��  �� ElementName$ sliderValue��$ Value�52      �� 53   
   -   5        $��sliderValue��53   3   $y�Hidden=�6   $��1��6&    � Maximum$	 100Q�6F   $ 0Q�6T   54      55      