   M S B A M L   `   `   `  ����B  >Kafedra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Kafedra.EventsAddingWindowsKafedra.EventsAddingWindows  5   	   0    *Kafedra.EventsAddingWindows.AddEventWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Kafedra.EventsAddingWindows     5   	     � Title$  AddEventWindow��5   	   $	��300��6    $	��300��6-   ���	#FF313338.��5	      � .+�5
   
   �� $#��Тип мероприятия��6   $
��Left=�6*   $��	10,10,0,0q�6E   $	��Top=�6X   $
��Bold"�6p   ���	#FFECECEC$��14(�6�   �� 5   
   -   5        $��EventTypeTextBox��5      $
��Left=�6*   $��23��6E   $��	10,30,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��120��6�   �� 5   
   $-��'Название мероприятия��6   $
��Left=�6/   $��	10,60,0,0q�6J   $	��Top=�6]   ���	#FFEAEAEA$
��Bold"�6�   $��14(�6�   �� 5   
   -   5        $��EventNameTextBox��5      $
��Left=�6*   $��23��6E   $��	10,80,0,0q�6Q   $
 Wrap=�6d   $	��Top=�6x   $	��120��6�   �� 5   
   $%��Дата мероприятия��6   $
��Left=�6+   $��
10,110,0,0q�6F   $	��Top=�6Z   $
��Bold"�6r   ���	#FFEDEDED$��14(�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��EventDatePicker��5      $
��Left=�6,   $��
10,130,0,0q�6G   $	��Top=�6[   $	��120��6s   �� 5   
   -   5        $��Сохранить��5      $
��Left=�6%   $��
10,160,0,0q�6@   $	��Top=�6T   $��86��6l   ���	#FF5865F2���	#FFECECEC$
��Bold"�6�   $��15(�6�   $��33��6�   5      5      