﻿#pragma checksum "..\..\EventsControllerPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "62AB9180173F08C53C625F5FC2D26A8E90AC5848460E88FAFF4FEE0D01E2279B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kafedra;
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


namespace Kafedra {
    
    
    /// <summary>
    /// EventsControllerPage
    /// </summary>
    public partial class EventsControllerPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox participantsComboBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox eventsComboBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AssignEventButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveAssignmentButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox guestsComboBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox eventsComboBox1;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AssignEventButton1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveAssignmentButton1;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\EventsControllerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid1;
        
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
            System.Uri resourceLocater = new System.Uri("/Kafedra;component/eventscontrollerpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EventsControllerPage.xaml"
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
            
            #line 15 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddParticipants_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 16 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditPartButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 17 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeletePartButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddEvent_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 19 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteEventButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.participantsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.eventsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.AssignEventButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\EventsControllerPage.xaml"
            this.AssignEventButton.Click += new System.Windows.RoutedEventHandler(this.AssignEventButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.RemoveAssignmentButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\EventsControllerPage.xaml"
            this.RemoveAssignmentButton.Click += new System.Windows.RoutedEventHandler(this.RemoveAssignmentButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            
            #line 25 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EventEdt_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 30 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddGuest_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 31 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 32 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 33 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddEvent_Click1);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 34 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteEventButton_Click1);
            
            #line default
            #line hidden
            return;
            case 17:
            this.guestsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 18:
            this.eventsComboBox1 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 19:
            this.AssignEventButton1 = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\EventsControllerPage.xaml"
            this.AssignEventButton1.Click += new System.Windows.RoutedEventHandler(this.AssignEventButton_Click1);
            
            #line default
            #line hidden
            return;
            case 20:
            this.RemoveAssignmentButton1 = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\EventsControllerPage.xaml"
            this.RemoveAssignmentButton1.Click += new System.Windows.RoutedEventHandler(this.RemoveAssignmentButton_Click1);
            
            #line default
            #line hidden
            return;
            case 21:
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(target));
            
            #line 39 "..\..\EventsControllerPage.xaml"
            this.dataGrid1.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataGrid1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 40 "..\..\EventsControllerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EventEdt_Click1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

