﻿#pragma checksum "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3D1C70D65043A845EFEAE2D4CFBE897E99636F3A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using VS_Presentation.ValueConverter;
using VS_Presentation.WPFControls;


namespace VS_Presentation.WPFControls {
    
    
    /// <summary>
    /// WPFControl_DateLimitCalendar
    /// </summary>
    public partial class WPFControl_DateLimitCalendar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 7 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_DateLimitCalendar userControl;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_TextButton HeadTitle;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MainCalenderContent;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MonthPicker;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MonthList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/VS_Presentation;component/wpfcontrols/wpfcontrol_datelimitcalendar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.userControl = ((VS_Presentation.WPFControls.WPFControl_DateLimitCalendar)(target));
            
            #line 8 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
            this.userControl.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.UserControl_PreviewMouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.HeadTitle = ((VS_Presentation.WPFControls.WPFControl_TextButton)(target));
            return;
            case 4:
            this.MainCalenderContent = ((System.Windows.Controls.ListView)(target));
            
            #line 73 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
            this.MainCalenderContent.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MainCalenderContent_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MonthPicker = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.MonthList = ((System.Windows.Controls.ListView)(target));
            
            #line 116 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
            this.MonthList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MonthList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 35 "..\..\..\WPFControls\WPFControl_DateLimitCalendar.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DateCheck_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

