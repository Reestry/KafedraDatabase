﻿#pragma checksum "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B43E57D890AF665336AB6A3B2238FE002CD538A09D266E718E920D61CAD64D8C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kafedra.Study.Group.AddingWindows;
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


namespace Kafedra.Study.Group.AddingWindows {
    
    
    /// <summary>
    /// EditGroupWindow
    /// </summary>
    public partial class EditGroupWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TeacherComboBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SpecializationComboBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GroupNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StudentsCountTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Kafedra;component/study/group/addingwindows/editgroupwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
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
            this.TeacherComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.SpecializationComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.GroupNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.StudentsCountTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\..\Study\Group\AddingWindows\EditGroupWindow.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

