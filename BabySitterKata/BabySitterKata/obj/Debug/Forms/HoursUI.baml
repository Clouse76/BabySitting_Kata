   M S B A M L   `   `   `  ����I  EBabySitterKata, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BabySitterKataBabySitterKata  5   	       BabySitterKata.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:BabySitterKata     5   	     � Title$  BabySitter Kata��5   	   $	��450��6!   $��465.335��6.    � WindowStartupLocation$ CenterScreen=�6>    � 
ResizeMode$ NoResize=�6c    � Icon$4 ./BabySitterKata;component/Resources/Green1.ico��6y   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FF6DBD1C��6    � Offset$ 0.612Q�6-   � 5      $ White��6   $ 1Q�6)   5      .��5      � .+�5   
   �� -   5        $��lblStartTime��5      $��Time Started:��6$   $
��Left=�6<   $��	48,86,0,0q�6W   $	��Top=�6j   �� 5   
   -   5        $��
lblEndTime��5      $��Time Ended:��6"   $
��Left=�68   $��
210,86,0,0q�6S   $	��Top=�6g   �� 5   
   -   5        $��	gbResults��5      $��Results��6$   $
��Left=�65   $	��197��6P   $��
64,153,0,0q�6]   $	��Top=�6q   $	��339��6�   �� 5   
   -   5        $��txtStartTime��5      $
��Left=�6&   $��23��6A   $��
128,86,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $��57��6�   $	��100��6�   �� 5   
   -   5        $��
txtEndTime��5      $
��Left=�6$   $��23��6?   $��
287,86,0,0q�6K   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $��50��6�   $	��101��6�   	� 5   
   -   5        $��rbEndPM��5      $��PM��6%   $
��Left=�62   $��
342,91,0,0q�6M   $	��Top=�6a   	 	� 	GroupName$	 	AMorPMEnd��6y   
 d� 	IsChecked$

 TrueY�6�   $	��102��6�   	� 5   
   -   5        $��rbEndAM��5      $��AM��6%   $
��Left=�62   $��
380,91,0,0q�6M   $	��Top=�6a   $	 	AMorPMEnd��6y   $	��103��6�   �� 5   
   -   5        $��lblTitle��5      $��Adventures in Babysitting��6    $
��Left=�6D   $��	86,10,0,0q�6_   $	��Top=�6r   $��24(�6�   �� 5   
   -	   5        $��btnExit��5      $
��Exit��6    $��	0,0,10,10q�6/   $	��108��6X   $��22��6g   $��Bottom=�6s   $��Right=�6�   $��75��6�   �� 5   
   -
   5        $��btnCalculate��5      $��	Calculate��6%   $��
0,0,113,10q�69   $	��107��6h   $��22��6w   $��Bottom=�6�   $��Right=�6�   $��75��6�   �� 5   
   -   5         ��Name$ 
lblBedTime��5      $��	Bed Time:��6$   $
��Left=�68   $��
61,117,0,0q�6S   $	��Top=�6g   �� 5   
   -   5         ��Name$ 
txtBedTime��5      $
��Left=�6&   $��23��6A   $��128,117,0,0q�6M   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $��57��6�   $	��104��6�   �� 5   
   $��Total Standard Hours:��6   $
��Left=�60   $��117,206,0,0q�6K   $	��Top=�6`   �� 5   
   $��Total Bed Hours:��6   $
��Left=�6+   $��117,237,0,0q�6F   $	��Top=�6[   �� 5   
   $��Total Overtime Hours:��6   $
��Left=�60   $��117,268,0,0q�6K   $	��Top=�6`   �� 5   
   $��Toal:��6   $
��Left=�6    $��117,299,0,0q�6;   $	��Top=�6P   �� 5    
   -   5        $��lblstandardHours��5       $��0��6(   $
��Left=�64   $��267,206,0,0q�6O   $	��Top=�6d   �� 5!   
   -   5        $��lblBedHours��5!      $��0��6#   $
��Left=�6/   $��267,237,0,0q�6J   $	��Top=�6_   �� 5"   
   -   5        $��lblOvertimeHours��5"      $��0��6(   $
��Left=�64   $��267,268,0,0q�6O   $	��Top=�6d   �� 5#   
   -   5        $��lblTotalHours��5#      $��0��6%   $
��Left=�61   $��267,299,0,0q�6L   $	��Top=�6a   	� 5$   
   -   5         	�Name$ rbBedPM��5$      $��PM��6'   $
��Left=�64   $��197,122,0,0q�6O   $	��Top=�6d   $	 	AMorPMBed��6|   $

 TrueY�6�   $	��105��6�   	� 5%   
   -   5        $ rbBedAM��5%      $��AM��6'   $
��Left=�64   $��235,122,0,0q�6O   $	��Top=�6d   $	 	AMorPMBed��6|   $	��106��6�   �� 5&   
   -   5        $ lblstandardPay��5&      $��0��6(   $
��Left=�64   $��334,206,0,0q�6O   $	��Top=�6d    �� ContentStringFormat$ C��6|   �� 5'   
   -   5        $ lblOvertimePay��5'      $��0��6(   $
��Left=�64   $��334,268,0,0q�6O   $	��Top=�6d   $ C��6|   �� 5(   
   -   5        $ 	lblBedPay��5(      $��0��6#   $
��Left=�6/   $��334,237,0,0q�6J   $	��Top=�6_   $ C��6w   �� 5)   
   -   5        $ lblGrossPay��5)      $��0��6%   $
��Left=�61   $��334,299,0,0q�6L   $	��Top=�6a   $ C��6y   �� 5*   
   $��Hours��6   $
��Left=�6    $��257,180,0,0q�6;   $	��Top=�6P   �� 5+   
   $	��Pay��6   $
��Left=�6   $��328,180,0,0q�69   $	��Top=�6N   5,      5-      