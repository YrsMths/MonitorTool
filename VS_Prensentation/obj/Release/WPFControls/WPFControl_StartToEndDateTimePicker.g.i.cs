#pragma checksum "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1275272AD0CC25AB9DB45D7F45701759AFD81C54"
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
using VS_Presentation.WPFControls;


namespace VS_Presentation.WPFControls {
    
    
    /// <summary>
    /// WPFControl_StartToEndDateTimePicker
    /// </summary>
    public partial class WPFControl_StartToEndDateTimePicker : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_BaseInputBox Box;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup PickerPop;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_DateTimePicker StartDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_DateTimePicker EndDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal VS_Presentation.WPFControls.WPFControl_TextButton OK_Button;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup AlarmPop;
        
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
            System.Uri resourceLocater = new System.Uri("/VS_Presentation;component/wpfcontrols/wpfcontrol_starttoenddatetimepicker.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WPFControls\WPFControl_StartToEndDateTimePicker.xaml"
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
            this.Box = ((VS_Presentation.WPFControls.WPFControl_BaseInputBox)(target));
            return;
            case 2:
            this.PickerPop = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 3:
            this.StartDateTimePicker = ((VS_Presentation.WPFControls.WPFControl_DateTimePicker)(target));
            return;
            case 4:
            this.EndDateTimePicker = ((VS_Presentation.WPFControls.WPFControl_DateTimePicker)(target));
            return;
            case 5:
            this.OK_Button = ((VS_Presentation.WPFControls.WPFControl_TextButton)(target));
            return;
            case 6:
            this.AlarmPop = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

