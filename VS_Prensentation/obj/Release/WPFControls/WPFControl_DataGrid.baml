   M S B A M L   `   `   `  ???? J  FVS_Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:VS_Presentation.WPFControlsVS_Presentation.WPFControls  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5    /VS_Presentation.WPFControls.WPFControl_DataGrid   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:VS_Presentation.WPFControls     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  0sys&clr-namespace:System;assembly=mscorlib  -   $??userControl??  ? 	Resources     DataGridRow.Height  DataGrid.BorderThickness u? Bottom u? Left u? Right
 u? Top   ColumnHeader.BorderThickness  DataGridRow.BorderThickness  DataGrid.BorderBrush ?? Color  ColumnHeader.Background  ColumnHeader.BorderBrush  DataGridRow.BorderBrush ! DataGridRow.Normal.Background !	 DataGridRow.Normal.Foreground $
  DataGridRow.MouseOver.Background $  DataGridRow.MouseOver.Foreground # DataGridRow.Selected.Background # DataGridRow.Selected.Foreground  LocalDataGrid ?? 
TargetType&   System.Windows.Controls.DataGrid ?? Property ?? Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader	 ?? ElementName
 ?? 
TargetType ??Name J? SnapsToDevicePixels ? UseLayoutRounding)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid ? ColumnDefinitions ? RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical ?? ConverterParameter#   RowDetailsScrollingConverter ?? 	Converter ?? RelativeSource ?? AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7	  1System.Windows.Controls.DataGridHeadersVisibility
 	  Row!   HeadersVisibilityConverter ?? Triggers P? Property P? Value*
  $System.Windows.Controls.DataGridCell ?? Triggers?? %b
  &	        &	      &	 1     &	 V     &	 {     &	 ?     &	 ?     &	 ?     &	 ?     &		 ?     &	
 ?     &	      &	     &	 &    &	 9    7  7  7  7   '      7  7  '  i    7  7  7  7  7   7  7 
 7  7  '
  ?    X?30Q?u? $ 1Q?$ 1Q?$ 1Q?$ 1Q?u? $ 0Q?$ 0Q?$ 1Q?$ 0Q?u? $ 0Q?$ 0Q?$ 0Q?$ 1Q??? $ #DDDDDD???? $ #f7f7f7???? $ #E9ECF1???? $ #DDDDDD???? $ #FFFFFF???? $ #000000???? $ #f2f2f2???? $ #000000???? $ #2E6FFE???? $ #FFFFFF???? "  .???  ?@ RowHeaderWidth$ 0Q???  ? ??8   ??  ?@ HeadersVisibility$ Column=???  ? ??8  ??  ? ??8  ??   ?@ HorizontalGridLinesBrush ?	#00E9ECF1??  ?@ VerticalGridLinesBrush ?	#00E9ECF1??  ?@?UseLayoutRounding . ??  ?@J?SnapsToDevicePixels . ??  ?@ AutoGenerateColumns .  ??  ?@ CanUserAddRows .  ??  ?@ CanUserReorderColumns .  ??  ?@ CanUserResizeColumns .  ??  ?@ CanUserResizeRows .  ??  ?@ CanUserSortColumns .  ??  ?@ GridLinesVisibility$
 None=???  ?@ 
IsReadOnly . ??  ?@ MinRowHeight8  ??  ?@ SelectionMode$ Single=??? "  .???  ?@J?SnapsToDevicePixels . ??  ? ?? ?#000000??  ? ?? ?? *
FontSize+$	 userControl????  ?@?Cursor$
 Hand????  ? ??$ 28????  ? ??$
 Bold"???  ? ?? ?? "
  .R??? $ BackgroundBorder??$
??Auto??$
??-1,0q?8??  8??  .  . ._??? $
??10,0q?$
??Left=?$??Center=??? "  .???  ? ??8   ??  ? ??8  ??  ?@J?SnapsToDevicePixels . ??  ?@?UseLayoutRounding . ??  ?@=?ErrorTemplate Z? ??  ? ??8  ??  ? ??8  ??  ?@ ValidationErrorTemplate ?? .R??? $??0,0,0,0q?$??Center=?
???  ??$??!????  ? ?? ?? "
  .R??? $ 
DGR_Border??-   $??0,0,0,-1q?#??z??#??z??#??z?? .  . ._?   ?? $
??Auto ??? $??* ? ?? $??* ?8?? ?? $
??Auto ?.+?  $??1??#??z # z   $??1??$??1??#y?z  ?? *AreRowDetailsFrozen+# Z # Z  ?? "    $??2??$ Vertical=?y??? *HeadersVisibility+# Z # Z  ?? "   P?  ?@J?IsMouseOver . . ??? 8   ? ???? 8   ? ??P?  ?@ 
IsSelected . . ??? 8   ? ???? 8   ? ???? " 
 .???  ? ?? ?? "
 
 .R??? $ Bg??
??????  . ._??? $
??Left=?$??Center=? P?  ?@
 
IsSelected . . ???  ? ??
 ???? ??  ? ?? ?#FFFFFF.??  -     Name$ dataGrid??$??2??#??[     HeadersVisibility$  Column=?!   SelectionMode$! Single=????? *ItemsSource+$	 userControl??