   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp4WpfApp4  5   	       WpfApp4.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp4     5   	   -   5          � Title$  FileMeneger��5   	   $	��500��6   $	��800��6*    � 	Resources 5	       �� 
TargetType �� Property �� Value��%x   '��       �� 5
   
   " ��6   .�5      ��  � ��$
 10,0q�6'   �� 5       � ��$ 35��6*   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $��* �6   .+�5   
   � $��0��6    5      �� 5      $��* �6    �� 5      $��8* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��6* �6'   .+�5      �� -   5         ��Name$ 
btn_GoBack��5   S   $
��Back��6   $
��5,10q�6(   $��35��66   $��90��6E   6g   �� 5      -   5         ��Name$ textBox_path��5   L   	 �� TextWrapping$
	 Wrap=�6   $��15,10q�6.   $��35��6=   $��1��6b   5       �� 5!      -   5        
 ��Name$
 
listV_Main��5!   "   $��5q�6   $��1��6f    � ContextMenu 5"      �� 5#      -   5        $��cm_ListView��5#   "   .@�5$      m� -   5         m�Name$
 Info��5$   1   $
��Info��6#   6?   m� 5%      -   5        $
 Copy��5%   1   $
��Copy��6#   6?   m� 5&      -   5        $ Paste��5&   2   $��Paste��6#   6A   m� 5'      -	   5        $ Delete��5'   3   $��Delete��6#   6C   m� 5(      -
   5        $ Create��5(   3   $��Create��6#   6C   m� 5)      -   5        $ 	Archiving��5)   6   $��	Archiving��6#   6I   m� 5*      -   5        $ rename��5*   3   $��Rename��6#   6C   5+       �� View 5-      �� 5.      .P�5/      ��  �� Width$	 500��6)   50      52      53      �� 54   
   -   5        $ btn_Console��54   r   $��5q�6   $��1��6   $��Console��6)   $��Right=�6;   $	��100��6W   $��50��6c   6�   55      56      