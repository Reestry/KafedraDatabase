   M S B A M L   `   `   `  ����B  >Kafedra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Kafedra.UserPagesKafedra.UserPages  5      +    %Kafedra.UserPages.UserDisciplinesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Kafedra.UserPages     5        N� Title$  UserDisciplinesPage��5	      .��5      � .+�5   
   �� .@�5      �� $)��#Создание элементов��6   .��5      � ���	#FF1A1A1A$��0,0,-4,0q�6.   $	��460��6@   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��SpecializationDataGrid��5      $
��Left=�6=   $��	10,80,0,0q�6X   $	��Top=�6k   $	��282��6�   $	��260��6�   5        5      -   5        $��DisciplineDataGrid��5      $
��Left=�69   $��
288,80,0,0q�6T   $	��Top=�6h   $	��282��6�   $	��260��6�   5      5      5      �� 5      $8��2Специальность -> Дисциплина��6   .��5      � ���	#FF1A1A1A$��0,0,-4,0q�6.   $	��460��6@   .+�5      �� $ ��Специальность��6   $��9,10,0,0q�64   $
��Left=�6F   
�������6a   �� 5      -   5        $��SpecializationComboBox��5       �� DisplayMemberPath$ SpecializationName��6=    �� SelectedValuePath$ SpecializationID��6d   $��
9,56,0,370q�6�   $
��Left=�6�   $	��171��6�   �� 5       $��Дисциплина��6   $��
201,10,0,0q�61   $
��Left=�6E   
�������6`   �� 5!      -   5        $��DisciplineComboBox��5!      $ DisciplineName��69   $ DisciplineID��6\   $��201,56,0,370q�6}   $
��Left=�6�   $	��172��6�   �� 5#      $��Тип работы��6   $��
399,10,0,0q�61   $
��Left=�6E   
�������6`   �� 5$      -   5        $��TypeWorkComboBox��5$      $ TypeWorkName��67   $ 
TypeWorkID��6X   $��399,56,0,370q�6w   $
��Left=�6�   $	��146��6�     5&      -   5        $��Spec_disc_TypeGrid��5&      $
��Left=�69   $��10,0,0,0q�6T   $��Center=�6f   $	��240��6�   $	��719��6�   5'      5)      5*      5,      5-      5.      