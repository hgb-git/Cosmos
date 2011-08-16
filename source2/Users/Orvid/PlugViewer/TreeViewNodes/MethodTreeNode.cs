﻿using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
using System.Windows.Forms;

namespace PlugViewer.TreeViewNodes
{
    public enum MethodType
    {
        BasicMethod,
        VirtualMethod,
        OverrideMethod,
    }

    internal class MethodTreeNode : OTreeNode
    {
        public MethodTreeNode(MethodReference definition, MethodType mType, Access mAccess)
        {
            this.def = definition;
            this.Text = NameBuilder.BuildMethodDisplayName(definition.Resolve());
            this.mType = mType;
            this.acc = mAccess;
            switch (mType)
            {
                case MethodType.BasicMethod:
                    {
                        switch (mAccess)
                        {
                            case Access.Public:
                                this.SelectedImageIndex = Constants.Method_Public;
                                this.ImageIndex = Constants.Method_Public;
                                break;
                            case Access.Private:
                                this.SelectedImageIndex = Constants.Method_Private;
                                this.ImageIndex = Constants.Method_Private;
                                break;
                            case Access.Protected:
                                this.SelectedImageIndex = Constants.Method_Protected;
                                this.ImageIndex = Constants.Method_Protected;
                                break;
                            case Access.Internal:
                                this.SelectedImageIndex = Constants.Method_Internal;
                                this.ImageIndex = Constants.Method_Internal;
                                break;
                        }
                    }
                    break;
                case MethodType.OverrideMethod:
                    {

                        switch (mAccess)
                        {
                            case Access.Public:
                                this.SelectedImageIndex = Constants.MethodOverride_Public;
                                this.ImageIndex = Constants.MethodOverride_Public;
                                break;
                            case Access.Private:
                                this.SelectedImageIndex = Constants.MethodOverride_Private;
                                this.ImageIndex = Constants.MethodOverride_Private;
                                break;
                            case Access.Protected:
                                this.SelectedImageIndex = Constants.MethodOverride_Protected;
                                this.ImageIndex = Constants.MethodOverride_Protected;
                                break;
                            case Access.Internal:
                                this.SelectedImageIndex = Constants.MethodOverride_Internal;
                                this.ImageIndex = Constants.MethodOverride_Internal;
                                break;
                        }
                    }
                    break;
                case MethodType.VirtualMethod:
                    {

                        switch (mAccess)
                        {
                            case Access.Public:
                                this.SelectedImageIndex = Constants.MethodVirtual_Public;
                                this.ImageIndex = Constants.MethodVirtual_Public;
                                break;
                            case Access.Private:
                                this.SelectedImageIndex = Constants.MethodVirtual_Private;
                                this.ImageIndex = Constants.MethodVirtual_Private;
                                break;
                            case Access.Protected:
                                this.SelectedImageIndex = Constants.MethodVirtual_Protected;
                                this.ImageIndex = Constants.MethodVirtual_Protected;
                                break;
                            case Access.Internal:
                                this.SelectedImageIndex = Constants.MethodVirtual_Internal;
                                this.ImageIndex = Constants.MethodVirtual_Internal;
                                break;
                        }
                    }
                    break;
            }
            Log.WriteLine("Method '" + this.Text + "' was loaded.");
        }

        public override TreeNodeType Type
        {
            get { return TreeNodeType.Method; }
        }

        private MethodReference def;
        private MethodType mType;
        private Access acc;

        public Access AccessModifier
        {
            get { return acc; }
        }

        public MethodType TypeOfMethod
        {
            get { return mType; }
        }

        public override object Definition
        {
            get { return (object)def; }
        }

        public override void ShowNodeInfo(RichTextBox rtb)
        {
            StringBuilder sb = new StringBuilder();
            switch (mType)
            {
                case MethodType.BasicMethod:
                    {
                        sb.AppendLine("Basic Method '" + this.Text + "' contains:");
                        sb.AppendLine(def.GenericParameters.Count.ToString() + " Generic Parameters,");
                        sb.AppendLine(def.Parameters.Count.ToString() + " Parameters,");
                        sb.AppendLine(def.Resolve().SecurityDeclarations.Count.ToString() + " Security Declarations,");
                        sb.AppendLine(def.Resolve().CustomAttributes.Count.ToString() + " Custom Attributes,");
                        sb.AppendLine(def.Resolve().Body.Instructions.Count.ToString() + " Instructions,");
                        sb.AppendLine(def.Resolve().Body.ExceptionHandlers.Count.ToString() + " Exception Handlers,");
                        sb.AppendLine(def.Resolve().Body.Variables.Count.ToString() + " Variables,");
                        sb.AppendLine("Has a calling convention of '" + def.CallingConvention.ToString() + "'");
                        sb.AppendLine();
                        sb.AppendLine();
                    }
                    break;
                case MethodType.OverrideMethod:
                    {
                        sb.AppendLine("Override Method '" + this.Text + "' contains:");
                        sb.AppendLine(def.GenericParameters.Count.ToString() + " Generic Parameters,");
                        sb.AppendLine(def.Parameters.Count.ToString() + " Parameters,");
                        sb.AppendLine(def.Resolve().SecurityDeclarations.Count.ToString() + " Security Declarations,");
                        sb.AppendLine(def.Resolve().CustomAttributes.Count.ToString() + " Custom Attributes,");
                        sb.AppendLine(def.Resolve().Body.Instructions.Count.ToString() + " Instructions,");
                        sb.AppendLine(def.Resolve().Body.ExceptionHandlers.Count.ToString() + " Exception Handlers,");
                        sb.AppendLine(def.Resolve().Body.Variables.Count.ToString() + " Variables,");
                        sb.AppendLine("Overrides " + def.Resolve().Overrides.Count.ToString() + " methods,");
                        sb.AppendLine("Has a calling convention of '" + def.CallingConvention.ToString() + "'");
                        sb.AppendLine();
                        sb.AppendLine();
                    }
                    break;
                case MethodType.VirtualMethod:
                    {
                        sb.AppendLine("Virtual Method '" + this.Text + "' contains:");
                        sb.AppendLine(def.GenericParameters.Count.ToString() + " Generic Parameters,");
                        sb.AppendLine(def.Parameters.Count.ToString() + " Parameters,");
                        sb.AppendLine(def.Resolve().SecurityDeclarations.Count.ToString() + " Security Declarations,");
                        sb.AppendLine(def.Resolve().CustomAttributes.Count.ToString() + " Custom Attributes,");
                        if (def.Resolve().HasBody)
                        {
                            sb.AppendLine(def.Resolve().Body.Instructions.Count.ToString() + " Instructions,");
                            sb.AppendLine(def.Resolve().Body.ExceptionHandlers.Count.ToString() + " Exception Handlers,");
                            sb.AppendLine(def.Resolve().Body.Variables.Count.ToString() + " Variables,");
                        }
                        else
                        {
                            sb.AppendLine("Doesn't have a body.");
                        }
                        sb.AppendLine("Has a calling convention of '" + def.CallingConvention.ToString() + "'");
                        sb.AppendLine();
                        sb.AppendLine();
                    }
                    break;
            }

            rtb.Text = sb.ToString();
        }
    }
}