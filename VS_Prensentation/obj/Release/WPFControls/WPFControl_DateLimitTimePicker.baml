   M S B A M L   `   `   `  ???? J  FVS_Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:VS_Presentation.WPFControlsVS_Presentation.WPFControls  @    :VS_Presentation.WPFControls.WPFControl_DateLimitTimePicker   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:VS_Presentation.WPFControls     -     >?Name$  userControl?? ? 	Resources    
StyleColor  DeactiveGray  ActiveBrush ?? Color  DeacetiveBrush ?? 
TargetType  	ClockItem ?? Property ?? Value 	 Hours  Minutes  Seconds??%?  &	        &	      &	 "     7   &	 ,     7  &	 6     &	 ?     7  &	     7  &	 ?    7  ??#2A1A87????#DDDDDD???? 8   ?? 8   ?? " ??.???  ? ??$ 0q???  ?@??Padding$ 0q???  ? ??
 ??????? .R??? $??1??$??1??$??75??$??28??$??0q?._??? $??SemiBold"?$??Center=?$??Center=?$??15(?8??  ???? *Value+?? .R??? $??1??$??1??$??75??$??28??$??0q?._??? $??SemiBold"?$??Center=?$??Center=?$??15(?8??  ???? *Value+?? .R??? $??1??$??1??$??75??$??28??$??0q?._??? $??SemiBold"?$??Center=?$??Center=?$??15(?8??  ???? *Value+.??? 
???????$	??230??$??90?? ? ColumnDefinitions ?? ?? ??  ? RowDefinitions ?? ?? ?? .+??? $??3??$??0??$??2??$	??时??$??Center=?$??Center=?#??[ ?? $??3??$??1??$??2??$	??分??$??Center=?$??Center=?#??[ ?? -    ??Name$ 
Hourpicker??$??3??$??90??$??0??$??Hidden=?.???? $??75??$??0q?
?????? #??[ $??Disabled=?$??Disabled=????? *	HoursList+	 ?? ElementName$	 userControl?????? " ??.???  ? ?? ?? 
 ?? 
TargetType"
 ??.R??? #??z??#??z??#??z??._???  ?? Triggers P?  P? Property ?@J?IsMouseOver P? Value . P?  ?@??
IsSelected . ???? .R??  ?Name$ WrapContainer??$??Center=?|?.   ? Orientation$ 
Horizontal=?$??0q?$??Disabled=?$??Disabled=??? -   $ Minutepicker??$??3??$??90??$??1??$??Hidden=?.???? $??75??$??0q?
?????? #??[ $??Disabled=?$??Disabled=????? *MinutesList+$	 userControl?????? " ??.???  ? ?? ?? "
 ??.R??? #??z??#??z??#??z??._???  P?  ?@J?IsMouseOver . P?  ?@??
IsSelected . ???? .R?? $ WrapContainer??$??Center=?|?.  $ 
Horizontal=?$??0q?$??Disabled=?$??Disabled=??? -   $ Secondpicker??$??3??$??90??$??2??$??Hidden=?.???? $??75??$??0q?
?????? #??[ $??Disabled=?$??Disabled=????? *SecondsList+$	 userControl?????? " ??.???  ? ?? ?? "
 ??.R??? #??z??#??z??#??z??._???  P?  ?@J?IsMouseOver . P?  ?@??
IsSelected . ???? .R?? $ WrapContainer??$??Center=?|?.  $ 
Horizontal=?$??0q?$??Disabled=?$??Disabled=??? $??0??$??3?????	#ABFFFFFF J? IsHitTestVisible .  ?? $??2??$??3?????	#ABFFFFFF .  ?? $??3??$??3??#??[ $??2??