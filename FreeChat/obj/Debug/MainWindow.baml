   M S B A M L   `   `   `  ����C  ?FreeChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:FreeChatFreeChat  5   	       FreeChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FreeChat     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � Title$ 登录��5	   	   $	��440��6   $	��420��6!      WindowStyle1#���   6-    � WindowStyle$ 
ToolWindow=�6T    � 	Resources 5
        WindowTemplateKey �� 
TargetType ��Name �� Triggers ]� 
Conditions �� Property	 �� Value
 �� 
TargetName �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%�  &	       &	  �    7  �� 5   
   " �64   .R�5      �� #��z��6   #��z��6A   #��z��6u   ._�5      � .+�5      �� .u�5      �� 5      �� 5      $ WindowResizeGrip��6!   $��Right=�6;   ��.  $y�	Collapsed=�6i   $��Bottom=�6�   5      5       5      ]� 5       5      �� 5       �@�
ResizeMode$	 CanResizeWithGrip=�6:   �� 5       �@�WindowState$	 Normal=�6;   ./�5      �� $
 WindowResizeGrip��63    � y�$ Visible=�6Q   5      5      �� 5   
   " �6%   .�5      ��  � ��# � ��6+   �� 5        � ��# � ��6+    5!      P� 5"       �@�
ResizeMode$ CanResizeWithGrip=�60   . �5#      ��  � ��8   61   5$      5&      5'      .��5(      �� $��Stretch=�6   $��Stretch=�6+   $��0,0,0,0q�6G   ��5)   
   �� 5*       �� 
StartPoint$	 0,1&�6"    �� EndPoint$	 1,0&�63   .��5+      �  � Color$ 	#FF1B84A3��6    � Offset$	 1.0Q�61   � 5,      $ 	#FF9ED4B8��6   $	 0.0Q�61   5-      .+�5/   
   ��  �� CornerRadius$	 100��50   ;   51      �� 54   
   -   5         ��Name$ loginHeader��54       �� Left$	 167��6&   
 �� Top$ 25��68   $��0q�6H   $��80��6\   $��80��6g    K� ZIndex$ 1��6s   $	��Top=�6�   $��2,5,2,2q�6�     BorderStyle1#���  6�   ��55      �� 56       �� ImageSource$&  Resources/Images/Headers/fqq.jpg��6   58      �� 5;   
   
���   �$��0q�6%   $	��Top=�69   $ 20��6Q   ���	#FFDDDDDD$ 54��6z   $
 68.5��6�   ._�5<      � $	��300��6   $ 62��6   $ 69.813��60   $��0,30.75,0,26.75q�6D   ��5=      �� 5>       �� Color$ 	#FFDDDDDD��6&   .+�5@      �� $��	用户名��6   $
��Left=�6&   $��	40,41,0,0q�6A   $	��Top=�6T   $��27.24��6l   $��127.457��6{   �� 5A      -   5         ��Name$ usernameTextBox��5A      $
��Left=�63   $��26��6N   $��40,69.24,0,0q�6Z   $��5,4,2,2q�6p    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��220��6�     TextBoxStyle1#���  6�   $��14(�6�   �� 5B      $��密码��6   $
��Left=�6%   $��40,96.24,0,0q�6@   $	��Top=�6V    J� RenderTransformOrigin$ 0.529,0.988&�6n   G� 5C      -   5         G�Name$ passwordBox��5C      $
��Left=�63   $��40,122.48,0,0q�6N   $��5,4,2,2q�6e   $	��Top=�6w   $	��220��6�   $��26��6�     PasswordBoxStyle1#���  6�   $��14(�6�   �� 5D      $
��Left=�6   $
��Wrap=�67   $��没有账号？注册��6K   $��108,236.76,0,-2q�6Z   ���	#FF005DFF6s   �� 5E      -   5          ��Name$  loginButton��5E      $��登录��6.   $
��Left=�6;   $	��Top=�6V   $	��155��6n   $��
72,177,0,0q�6z     ButtonStyle1#���  6�   $��36.76��6�   6�   5F      5G      �� 5H   
   -   5        $	��|||��5H      ! �� Right$! 6��6   $ 0.5,0.5&�6_   $��14(�6   ���	#FFDDDDDDz�5I      V� 5J      .~�5K      �� " �� Angle$" 90Q�6&   T� 5L      5M      5O      5P      5Q      