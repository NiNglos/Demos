   M S B A M L   `   `   `  ����D  @Transport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Transport	Transport  5   	       Transport.TestReport   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Transport     5   	     � Title$  Тест��5   	   $	��650��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5	      � ���	#FFA8A8A8.+�5   
   � $��80��6   $	��Top=�6   ��5      �� 5       j� Stretch$ UniformToFill=�6    �� ImageSource$ Resourses/vopr.jpg��65   .+�5      �� $
��Sans)�6   $��30(�6*   $��Center=�68   $��	0,20,0,20q�6U   
�������*(Отчет по тестированию5      5      � 5   
   $��0,80,0,0q�6   .+�5      �� ��5      �� 5      $ UniformToFill=�6!   $ Resourses/fon.png��69   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ txtFIO��5       $��Times New Roman)�61   $��20(�6N   $��
30,30,30,0q�6\   $
��Wrap=�6p   @>Студент: Будилин Никита Андреевич�� 5      Группа: 4ПКС-116�� 5      '%Количество баллов: 265       �� 5!      $��Times New Roman)�6!   $��20(�6>   $��
30,20,30,0q�6L   $
��Wrap=�6`   $
��Bold"�6t   $��Center=�6�   Ваша оценка:5#      �� 5$      -   5        $ txtMark��5$       $��Times New Roman)�62   $��20(�6O   $��
30,20,30,0q�6]   $
��Wrap=�6q   $
��Bold"�6�   $��Center=�6�   5 (отлично)5&      �� 5(      -   5        $ txtQuestions��5(       $��Times New Roman)�66   $��20(�6S   $��
30,30,30,0q�6a   $
��Wrap=�6u   Ваши ответы:�� 5*      5+      �� 5-      -   5        $ txtQuestion_5��5-       $��Times New Roman)�67   $��20(�6T   $��
30,30,30,0q�6b   $
��Wrap=�6v   5/      �� 51      -   5        $ txtQuestion_6_1��51       $��Times New Roman)�69   $��20(�6V   $��
30,30,30,0q�6d   $
��Wrap=�6x   53      &   System.Windows.Controls.DataGrid  54      -   5          Name$ gridQuestion_6��54      $��
30,30,30,0q�67   {�.     HeadersVisibility$
 None=�6]   $��Times New Roman)�6v   $��20(�6�      CanUserReorderColumns .  	   CanUserSortColumns	 .  
   MaxColumnWidth$
 70Q�6�      MinColumnWidth$ 50Q�6�   $
��Left=�6     CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ 50 64  55      �� 58      -   5        $
��Sans)�58      $��30(�6/   $��60��6=   $	��366��6I   
�������$��
30,40,30,0q�6i   $1��+Завершить тестирование��6}   $��Center=�6�   ��59      �� 5:      5=      �� 5>      �� 5?      �� 6*   5@      5A      5B      5C      5E      5F      