﻿#pragma checksum "..\..\..\Pages\DeleteChoice.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0652154138BEB986AF176B7FADCB8919E8DFF9E367E9CF24C6146164E6400B5F"
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
using WpfApp1.Pages;


namespace WpfApp1.Pages {
    
    
    /// <summary>
    /// DeleteChoice
    /// </summary>
    public partial class DeleteChoice : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Pages\DeleteChoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteManufButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Pages\DeleteChoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteModelButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Pages\DeleteChoice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteChoiceCancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/pages/deletechoice.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\DeleteChoice.xaml"
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
            this.DeleteManufButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Pages\DeleteChoice.xaml"
            this.DeleteManufButton.Click += new System.Windows.RoutedEventHandler(this.DeleteManufClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DeleteModelButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Pages\DeleteChoice.xaml"
            this.DeleteModelButton.Click += new System.Windows.RoutedEventHandler(this.DeleteModelClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeleteChoiceCancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Pages\DeleteChoice.xaml"
            this.DeleteChoiceCancelButton.Click += new System.Windows.RoutedEventHandler(this.DeleteChoiceCancelClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

