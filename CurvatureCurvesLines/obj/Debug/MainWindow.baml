   M S B A M L   `   `   `  ����O  KCurvatureCurvesLines, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CurvatureCurvesLinesCurvatureCurvesLines  5   	   %    CurvatureCurvesLines.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:CurvatureCurvesLines     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5	         	blueFatin 
� RadiusY 
� RadiusX 
� GradientOrigin � Color � Offset  OliveYellowGradientBrush �� 
StartPoint �� EndPoint��%�  &	        &	 �     
� 5
   
   $ 0.573Q�60   $ 0.667Q�6@   $ 
1.01,1.035&�6P   .��5      � $ 	#FF06183C��6   $
 0.81Q�6-   � 5      $ 	#FF0D60A3��6   5      �� 5   
   $	 0,0&�5      $	 1,1&�6   .��5      � $ DarkOliveGreen��6   $ 0Q�64   � 5      $ Bisque��6   $	 0.5Q�6*   � 5      $ DarkOliveGreen��6   $ 1Q�62   5      5      .��5      ]� ��. #��[  6%   .+�5   
   �� -   5        	 ��Name$	 	borderTop��5      $	��Top=�6$   $��Stretch=�69   $
��auto��6W   #��[ 6e   $��2q�6�   
 �� CornerRadius$
 5,5,5,5��6�   $��2q�6�   ._�5      �� #��[  6   .+�5      �� $�� University of Technology��6   $��1q�6=   #��[ 6H   $��16(�6   $
��Bold"�6�   $��Center=�6�   �� 5      $�� Fatin Sadiq Alkinani��6   $��1q�69   #��[ 6D   $��16(�6{   $
��Bold"�6�   $��Center=�6�   5      5      �� 5   
   -   5        $	 borderRight��5      $��Bottom=�6&   $��2q�6>   $��Center=�6I   $
��auto��6g   $
��auto��6u   #��[ 6�   $��2,2,2,2q�6�   $
 5,5,5,5��6�   ._�5      �� #��[  6    �� Orientation$ 
Horizontal=�6F   $
��auto��6_   $��RightToLeft=�6m   .+�5      �� -   5         ��Name$ 
btnProceed��5      $��Proceed��6-   $��2q�6?   $��	ExtraBold"�6J   $��16(�6a   6�   5      5"      �� 5#   
   -   5        $	 borderCenter��5#      $��2q�6(   $
��Left=�63   $
��auto��6I   $��Stretch=�6W   #��[ 6v   $��2,2,2,2q�6�   $
 5,5,5,5��6�   ._�5$      � -   5         �Name$ mainArea��5$      #��[  6*   .+�5%      �� $��Bottom=�6   $��2q�62   $��Center=�6=   $
��auto��6[   $
��auto��6i   #��[ 6w   $��2,2,2,2q�6�   $
 5,5,5,5��6�   ._�5&      �� #��[  6%   $
��auto��6O   $��RightToLeft=�6]   .+�5'      �� $��Original Image��6$   #��[ 6:   �� 5(      -   5        $��original��5(       62   5)      5*      �� 5+      $��Bottom=�6   $��2q�62   $��Center=�6=   $
��auto��6[   $
��auto��6i   #��[ 6w   $��2,2,2,2q�6�   $
 5,5,5,5��6�   ._�5,      �� #��[  6%   $
��auto��6O   $��RightToLeft=�6]   .+�5-      �� $��Canny��6$   #��[ 61   �� 5.      -   5        $��canny��5.       6/   5/      50      �� 51      $��Bottom=�6   $��2q�62   $��Center=�6=   $
��auto��6[   $
��auto��6i   #��[ 6w   $��2,2,2,2q�6�   $
 5,5,5,5��6�   ._�52      �� #��[  6%   $
��auto��6O   $��RightToLeft=�6]   .+�53      �� $��	File Plot��6$   #��[ 65   �� 54      -   5        $��filePlot��54       62   55      56      5:      5;      5<      5=      