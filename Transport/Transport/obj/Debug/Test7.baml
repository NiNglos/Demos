   M S B A M L   `   `   `  ����D  @Transport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Transport	Transport  5   	       Transport.Test7   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Transport     5   	     � Title$  Тест��5   	   $	��650��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5	      � ���	#FFFFF3DA.+�5
   
   � $��80��6   $	��Top=�6   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF85B8AD��6#    � Offset$ 0Q�65   � 5      $ 	#FFFAE2AD��6#   $ 1Q�65   5      .+�5      �� $
��Sans)�6   $��30(�6*   $��Center=�68   $��	0,20,0,20q�6U   Задание №65      5      � 5   
   $��0,80,0,0q�6   .+�5      �� .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ txtblQestion��5       $��Times New Roman)�67   $��20(�6T   $��
30,30,30,0q�6b   $
��Wrap=�6v   �� 5      .�6#   CAСбалансируйте транспортную задачу.6G   �� 5      LJ(количествово баллов за задание - 4 балла)5      �� 5      $��Times New Roman)�6    $��20(�6=   $��
30,10,30,0q�6K   $
��Wrap=�6_   �� 5      B@Дана матрица стоимостей перевозки:�� 5       5!      &   System.Windows.Controls.DataGrid  5"      -   5          Name$ gridExample��5"      $
��30,0q�64   	   HeadersVisibility$
	 None=�6B   $��Times New Roman)�6[   $��20(�6x   
   CanUserReorderColumns
 .     CanUserResizeColumns .     CanUserSortColumns .     MinColumnWidth$ 50Q�6�      MaxColumnWidth$ 70Q�6�      CanUserAddRows .  {�.  $
��Left=�6-     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ 50 6H  5$      �� 5'      $��Times New Roman)�6    $��20(�6=   $��
30,10,30,0q�6K   $
��Wrap=�6_   �� 5(      " Сбалансируйте ее:�� 5*      5+      �� 5,      $ 
Horizontal=�6!   .+�5-      �� -   5         ��Name$ 	btnAddRow��5-   !   $��Times New Roman)�64   $��20(�6Q   $
��Left=�6_   $��30,0,0,0q�6z   ���	#FFFFE4AD���	#FFFDB729$	��180��6�   .��6�   !+ Добавить строку5/      �� 50      -   5        $ btnAddColumn��50   !   $��Times New Roman)�67   $��20(�6T   $
��Left=�6b   $
��30,0q�6}   ���	#FFFFE4AD���	#FFFDB729$	��190��6�   .��6�   #!+ Добавить столбец52      54      �� 55      $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�56      �� -   5        $ btnDeleteRow��56   !   $��Times New Roman)�67   $��20(�6T   $
��Left=�6b   $��30,0,0,0q�6}   ���	#FFFFE4AD���	#FFFDB729$	��180��6�   .��6�   !– Удалить строку58      �� 59      -   5        $ btnDeleteColumn��59   !   $��Times New Roman)�6:   $��20(�6W   $
��Left=�6e   $
��30,0q�6�   ���	#FFFFE4AD���	#FFFDB729$	��190��6�   .��6�   #!– Удалить столбец5;      5<        5>      -   5        $ 
gridAnswer��5>      $��
30,30,30,0q�63   $
	 None=�6G   $��Times New Roman)�6`   $��20(�6}   
 .   .  $ 70Q�6�   $ 50Q�6�   $
��Left=�6�    .  $ 50 6  5@      �� 5B      -   5        $
��Sans)�5B      $��30(�6/   $��60��6=   $	��220��6I   ���	#FFD6280E
�������$��271,40,271,0q�6�   $��Продолжить��6�   ��5C      �� 5D      $ 0.5,1&�62    � MappingMode$ RelativeToBoundingBox=�6C   $ 0.5,0&�6g   .��5E   "   � $ 	#FF82B7AD��6/   � 5F   "   $ 	#FFFFE4AD��6/   $ 1Q�6A   5G      5I      �� 5J      �� 5K      �� 6*   5L      5M      5N      5O      5P      5Q      