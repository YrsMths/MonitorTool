   M S B A M L   `   `   `  ????J  FVS_Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:VS_Presentation.DialogControlsVS_Presentation.DialogControls  5   	   I)clr-namespace:VS_Presentation.WPFControlsVS_Presentation.WPFControls  5   	   6    0VS_Presentation.DialogControls.Window_MessageBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:VS_Presentation.DialogControls     5   	   Awpfcontrols)clr-namespace:VS_Presentation.WPFControls     5   	   -   5          ?Name$  window??5   	    ? Title$ Window_MessageDialog??5	   	   $	??200??6&   $	??350??63    ? WindowStartupLocation$ CenterScreen=?6@    ? 
ResizeMode$ NoResize=?6f    ? ShowInTaskbar .   ? ShowActivated .  J? RenderTransformOrigin$ 0.5,0.5&?6?    ? Topmost .  ? 	Resources 5
      	 ?? MergedDictionaries
 ?? Source?? 5   
   	 5      ?? 5      $S
 M/VS_Presentation;component/ResourceDictionary/Dictionary_WindowBaseStyle.xaml??6%   %    5      .??5      ? 
??????? ? RowDefinitions 5   
   ?? 5      $??40 ?6   ?? 5      $??90 ?6   ?? 5      $??40 ?6   ?? 5       ? ColumnDefinitions 5   
   ?? 5      $??40 ?6   ?? 5      ?? 5      $	??100 ?6   ?? 5      ?? 5      $??40 ?6   .+?5    
   ?? $??0??6   $??5??6!   ???#F8F8F865   ?? 5!   
   $??0??6   $??5??6   ???#DDDDDD	 ?? X1$ 0??6A   	 ?? X2$	 350??6H   	 ?? Y1$ 40??6Q   	 ?? Y2$ 40??6Y   ?? 5"   
   $??0??6   $??5??6!   $??SemiBold"?65   $??Center=?6y   $??Center=?6?   ??6K   ?? *
MsgTitle+ ?? ElementName$ window??6?   ?? 5#   
   $??	20,0,20,0q?6   $??1??6*   $??5??67   $??Center=?6K   $??Center=?6h   $
??Auto=?6?   .??5$      ?? $??12(?6H   $
??Wrap=?6X   ??6   ?? *
MsgContent+$ window??6X   5%      ?? 5&   
   $??2??6   $??2??6    J? Effect 5'      3  -System.Windows.Media.Effects.DropShadowEffect  5(         ShadowDepth$ 0Q?6#      
BlurRadius$ 13Q?63      Opacity$	 0.1Q?6C   ._?5*      7   1VS_Presentation.WPFControls.WPFControl_TextButton  7   1VS_Presentation.WPFControls.WPFControl_TextButton   TextButtonClick$ OK_Click??6w  $??6??60   $??1??6=      BorderRadius$ 15Q?6N   $??Center=?6`   $??Center=?6~      ButtonHeight$ 30Q?6?      ButtonWidth$	 100Q?6?      ButtonBorderThickness$ 1q?6?      BorderBrush ?#E0E7E9   TextContent$ 确定??6?      RegularBackground ?#3cb07f   MouseOverBackground ?#4abe8e   MouseDownBackground ?#2a7d5a    	TextBrush
  ?????$??12(?6i  6w  5+      5,      5-      