   M S B A M L   `   `   `  ����  FinanceTracker    FinanceTracker.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Budżet Domowy��5   	   $	��600��6   $	��900��6,   ���#f4f6f8 � WindowStartupLocation$ CenterScreen=�6M   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� ���#2c3e50$��0��6*   .+�5      �� $��FinanceTracker��6   
�������$��20(�6A   $
��Bold"�6O   $��10q�6a   �� 5      $
��10,5q�6   �� 5      $��Budżet��6   $��10q�6&   
�������
������ 
������ $��SemiBold"�6x    � Cursor$
 Hand��6�   �� 5      $��Raporty��6   $��10q�6'   
�������
������ 
������ $��SemiBold"�6y   $
 Hand��6�   �� 5      $��
Ustawienia��6   $��10q�6*   
�������
������ 
������ $��SemiBold"�6|   $
 Hand��6�   5      �� 5   
   $��1��6   $��20q�6%   .+�5      �� $��Nowa transakcja��6   $��18(�6/   $
��Bold"�6=   $��0,0,0,10q�6O   � 5      $��0,0,0,10q�6   .+�5      �� -   5         ��Name$ DescriptionTextBox��5      $	��200��66   $��30��6B   $��5q�6N    � ToolTip$ Opis transakcji��6Y   �� 5      -   5        $ AmountTextBox��5      $	��100��61   $��30��6=   $��5q�6I   $ Kwota��6T   �� 5      -   5         ��Name$ TypeComboBox��5      $	��120��61   $��30��6=   $��5q�6I    �� SelectedIndex$ 0��6T   .@�5      �� $��	Przychód��6#   �� 5      $��Wydatek��6#   5      �� 5       -   5        $��Dodaj��5       $	��100��6)   $��30��65   $��5q�6A   ���#27ae60
�������6~   5!      �� 5#      $
��0,10q�6   �� 5%      $��Lista transakcji��6   $��16(�60   $
��Bold"�6>   $��0,0,0,5q�6P   &   System.Windows.Controls.DataGrid  5&      -   5          Name$ TransactionsDataGrid��5&      	   AutoGenerateColumns$	 False��5'      
�������
   
IsReadOnly$

 True��5)      $	��350��5*      $��0,0,0,10q�5+         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$
 Opis��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Description+  5/      $ Kwota��6)    68   �� *Amount+  50      $	 Typ��6)    66   �� *Type+  51      $
 Data��6)    67   �� *Date+4  .System.Windows.Controls.DataGridTemplateColumn  52      $ Usuń��6-      CellTemplate 53      �� 54      .R�55   "   �� -   5        $��🗑️��55   )   $��5q�6W   56      58      5:      �� 5<      -   5         ��Name$ BalanceTextBlock��5<      $��18(�5=      $
��Bold"�5>       �� TextAlignment$ Right=�5?      $��Right=�5@      5A      5B      5C      