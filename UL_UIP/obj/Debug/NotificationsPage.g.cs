﻿#pragma checksum "..\..\NotificationsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "77F37335FA4589B7448775714B26832A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
using UL_UIP;


namespace UL_UIP {
    
    
    /// <summary>
    /// NotificationsPage
    /// </summary>
    public partial class NotificationsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\NotificationsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\NotificationsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox NotificationsList;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\NotificationsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MarkAsRead;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\NotificationsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Delete;
        
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
            System.Uri resourceLocater = new System.Uri("/UL_UIP;component/notificationspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NotificationsPage.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.NotificationsList = ((System.Windows.Controls.ListBox)(target));
            
            #line 14 "..\..\NotificationsPage.xaml"
            this.NotificationsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.NotificationsList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MarkAsRead = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.Delete = ((System.Windows.Controls.Grid)(target));
            
            #line 27 "..\..\NotificationsPage.xaml"
            this.Delete.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Delete_MouseEnter);
            
            #line default
            #line hidden
            
            #line 27 "..\..\NotificationsPage.xaml"
            this.Delete.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Delete_MouseLeave);
            
            #line default
            #line hidden
            
            #line 27 "..\..\NotificationsPage.xaml"
            this.Delete.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Delete_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

