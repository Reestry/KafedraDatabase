﻿#pragma checksum "..\..\..\..\Study\Group\GroupPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AFF3F0FB1D503EBAE144F6C9E55E351BD6A2641A27B7979EF4EE173EB9128971"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kafedra.Study.Group;
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


namespace Kafedra.Study.Group {
    
    
    /// <summary>
    /// GroupPage
    /// </summary>
    public partial class GroupPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Study\Group\GroupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid _groupDataGrade;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Study\Group\GroupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GroupsComboBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Study\Group\GroupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GroupsComboBox1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Study\Group\GroupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid _groupData_GetGrade;
        
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
            System.Uri resourceLocater = new System.Uri("/Kafedra;component/study/group/grouppage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Study\Group\GroupPage.xaml"
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
            this._groupDataGrade = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\..\..\Study\Group\GroupPage.xaml"
            this._groupDataGrade.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this._groupDataGrade_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 18 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddGroupButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 19 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditGroupButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteGroupButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 26 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddGroupButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GroupsComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 27 "..\..\..\..\Study\Group\GroupPage.xaml"
            this.GroupsComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupsComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GroupsComboBox1 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\..\..\Study\Group\GroupPage.xaml"
            this.GroupsComboBox1.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupsComboBox1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this._groupData_GetGrade = ((System.Windows.Controls.DataGrid)(target));
            
            #line 38 "..\..\..\..\Study\Group\GroupPage.xaml"
            this._groupData_GetGrade.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this._groupDataGrade_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 41 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddGrade_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 42 "..\..\..\..\Study\Group\GroupPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditGrade_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

