﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdentityHost.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IdentityHost.Properties.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 记录已经存在：{0} 的本地化字符串。
        /// </summary>
        internal static string AlreadyExists {
            get {
                return ResourceManager.GetString("AlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 文件不合法 的本地化字符串。
        /// </summary>
        internal static string FileInvalid {
            get {
                return ResourceManager.GetString("FileInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 资源不存在 的本地化字符串。
        /// </summary>
        internal static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 不支持多级分类 的本地化字符串。
        /// </summary>
        internal static string NotSupport {
            get {
                return ResourceManager.GetString("NotSupport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 上级分类不存在 的本地化字符串。
        /// </summary>
        internal static string ParentNotFound {
            get {
                return ResourceManager.GetString("ParentNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 重复的操作 的本地化字符串。
        /// </summary>
        internal static string Repeat {
            get {
                return ResourceManager.GetString("Repeat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 测试 的本地化字符串。
        /// </summary>
        internal static string Test {
            get {
                return ResourceManager.GetString("Test", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 生效时间必须大于当前时间 的本地化字符串。
        /// </summary>
        internal static string TimeTooEarly {
            get {
                return ResourceManager.GetString("TimeTooEarly", resourceCulture);
            }
        }
    }
}
