   M S B A M L   `   `   `  ????J  FVS_Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:VS_Presentation.WPFControlsVS_Presentation.WPFControls  5      O,clr-namespace:VS_Presentation.ValueConverterVS_Presentation.ValueConverter  5      >    8VS_Presentation.WPFControls.WPFControl_TextBoxWithButton   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:VS_Presentation.WPFControls     5      DConvertBase,clr-namespace:VS_Presentation.ValueConverter     5      -   5          >?Name$  userControl??5       ? 	Resources 5	      D   >VS_Presentation.ValueConverter.TextBoxWithButtonWidthConverter #  textBoxWithButtonWidthConverter??%   &	          5
   
   66   5      .??5      ? |?. ??6   ?? *Width+ ?? ElementName$ userControl??.+?5   
   ?? -   5         ??Name$ Border??5       ?? CornerRadius$ 2??6!   ??62   ?? *Width+$ TextBoxWithButton?? ?? Mode$ OneWay=??? 5   
    ?? 
IsReadOnly . 
?????? $??Center=?6s   $??0q?6?   ???#333333$??9,7,35,6q?6?   ??6=   ?? *TextBoxText+$ userControl????6?   ?? *Width+$ TextBoxWithButton??$ OneWay=? ?? 	Converter# [  .??6N   5        HintText j? 
AlignmentX	 ?? Opacity
 j? Stretch j? TileMode !? Visual??%?   &	       !? 5      $
 Left=?6/   $		 0.5Q?6A   $

 None=?6O   $
 None=?6^    5      ?? 5      $??12(?6$   ???#999999??6G   ?? *Hint+$ TextBoxWithButton??5      5      .??6!   ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ?????63    ?? Triggers 5      P? 5       P? Property ? ?? P? Value 62   Z? . ?5      ??  ? ??# [ 6;   5      P? 5       ? ??$  ??62   . ?5      ??  ? ??# [ 6;   5      P? 5        ?@J?	IsFocused . . ?5!      ??  ? ??
 ?????6;   5"      5$      .??5%      5&      8   2VS_Presentation.WPFControls.WPFControl_ImageButton  5'   
   -   5          Name$ ImageButton??5'   (   8   2VS_Presentation.WPFControls.WPFControl_ImageButton   ImageButtonClick$" ImageButton_ImageButtonClick??6?   $??16??6=   $??0,0,9,7q?6H      ButtonHeight$ 16Q?6Y      ButtonWidth$ 16Q?6k      
ImageWidth$ 16Q?6|      ImageHeight$ 16Q?6?   $??Right=?6?   $??Bottom=?6?   $??16??6?      RegularBackground
 ?   ? ? Cursor 6+  ?? *ButtonCursor+$ userControl??5(      5)      