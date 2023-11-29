﻿#pragma checksum "..\..\..\..\Study\Teacher\TeacherPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F1CDC07FF9AEF7BA07CF0BB388EAD207A6311683D7016FBE92C8D3059B52F01E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kafedra.Study.Teacher;
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


namespace Kafedra.Study.Teacher {
    
    
    /// <summary>
    /// TeacherPage
    /// </summary>
    public partial class TeacherPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid _teachersGrid;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TeachersComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid _teachers_disciplinesGrid;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SupTeachersComboBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SupGroupComboBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid _teachers_Group_ass;
        
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
            System.Uri resourceLocater = new System.Uri("/Kafedra;component/study/teacher/teacherpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
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
            this._teachersGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 17 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this._teachersGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataGrid1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 21 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 22 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 32 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AssignTeacher_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 33 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteAssignTeacher_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TeachersComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this.TeachersComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TeachersComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this._teachers_disciplinesGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 37 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this._teachers_disciplinesGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataGrid1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 40 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 42 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SupTeachersComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 52 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this.SupTeachersComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TeachersComboBox_SelectionChanged1);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SupGroupComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this.SupGroupComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupsComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 54 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AssignSupGroup_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this._teachers_Group_ass = ((System.Windows.Controls.DataGrid)(target));
            
            #line 56 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            this._teachers_Group_ass.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataGrid1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 59 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 61 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTeacherButton_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 65 "..\..\..\..\Study\Teacher\TeacherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteAssignSupGroup_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

