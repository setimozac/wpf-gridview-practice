   M S B A M L   `   `   `  ����L  HListViewPractice2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ListViewPractice2ListViewPractice2  5   	   "    ListViewPractice2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ListViewPractice2     5   	   -   5          � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ listV��5   >   $��0��6M   $��0��6Z   $��2��6j   $��10,20q�6{   5      �� 5   
   $��1��6   $��0��6%   $��4��62   .+�5      � $��
70,80,70,0q�6    5      �� 5      $��* �6'   �� 5      $��* �6'    5!      �� 5"      $��* �6$   �� 5#      $��* �6$   .+�5&      �� $
��Name��6   $��0��6(   6:   �� 5'      -   5         ��Name$ tBoxName��5'      $��1��6,   $��10q�6<   6J   �� 5)      $	��Age��6   $��0��6'   $��1��67   6F   �� 5*      -   5        $ tBoxAge��5*      $��1��6+   $��1��6;   $��10q�6H   6V   5+      �� 5,      -   5        $��
Add Person��5,   *   $��70,70,70,10q�6?   6V   �� 5-      -   5         ��Name$ 	btnDelete��5-   -   $��Delete��6@   $��
70,0,70,10q�6Q   6g   �� 5.      -   5        $ 	btnUpdate��5.   -   $��update Person��6@   $��
70,0,70,10q�6X   6n   �� 5/      -   5        $ btnClear��5/   ,   $��clear��6>   $��
70,0,70,10q�6N   6d   50      52      53      