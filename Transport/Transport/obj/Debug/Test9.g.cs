#pragma checksum "..\..\Test9.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C5EAB1B39F33F23621EA4BAAE1AAB15A01F59C48373C4EE5FF2B3302AFB60527"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using Transport;


namespace Transport {
    
    
    /// <summary>
    /// Test9
    /// </summary>
    public partial class Test9 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblQestion;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridExample;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddRow;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddColumn;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteRow;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteColumn;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridAnswer;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Test9.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt;
        
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
            System.Uri resourceLocater = new System.Uri("/Transport;component/test9.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Test9.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.txtblQestion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.gridExample = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnAddRow = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\Test9.xaml"
            this.btnAddRow.Click += new System.Windows.RoutedEventHandler(this.btnAddRow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAddColumn = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\Test9.xaml"
            this.btnAddColumn.Click += new System.Windows.RoutedEventHandler(this.btnAddColumn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDeleteRow = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\Test9.xaml"
            this.btnDeleteRow.Click += new System.Windows.RoutedEventHandler(this.btnDeleteRow_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnDeleteColumn = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\Test9.xaml"
            this.btnDeleteColumn.Click += new System.Windows.RoutedEventHandler(this.btnDeleteColumn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.gridAnswer = ((System.Windows.Controls.DataGrid)(target));
            
            #line 63 "..\..\Test9.xaml"
            this.gridAnswer.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.gridAnswer_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txt = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\Test9.xaml"
            this.txt.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txt_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 74 "..\..\Test9.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

