﻿#pragma checksum "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2ADAE1438B6F5E89C5018F49117E01E515C5D486A93C90850E831297C827FAC4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kafedra.Study.Teacher.TeacherWindows;
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


namespace Kafedra.Study.Teacher.TeacherWindows {
    
    
    /// <summary>
    /// TeacherAssignWindow
    /// </summary>
    public partial class TeacherAssignWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Group;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeWork_Specialization_Discipline;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AverageTime;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GroupName;
        
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
            System.Uri resourceLocater = new System.Uri("/Kafedra;component/study/teacher/teacherwindows/teacherassignwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
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
            this.Group = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TypeWork_Specialization_Discipline = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.AverageTime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.GroupName = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 18 "..\..\..\..\..\Study\Teacher\TeacherWindows\TeacherAssignWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

