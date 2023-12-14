//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v13.0.0+9dfb300
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	/// <summary>UNSDG Single</summary>
	[PublishedModel("uNSDGSingle")]
	public partial class UNsdgsingle : PublishedContentModel, IContentArea, IPageMetadata
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		public new const string ModelTypeAlias = "uNSDGSingle";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<UNsdgsingle, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public UNsdgsingle(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// SDG Focus
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sDGFocus")]
		public virtual string SDgfocus => this.Value<string>(_publishedValueFallback, "sDGFocus");

		///<summary>
		/// SDG Logo Image: The logo for this SDG
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sDGLogoImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops SDglogoImage => this.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(_publishedValueFallback, "sDGLogoImage");

		///<summary>
		/// SDG Number: The SDG ID as a computer-readable number
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[ImplementPropertyType("sDGNumber")]
		public virtual int SDgnumber => this.Value<int>(_publishedValueFallback, "sDGNumber");

		///<summary>
		/// SDG Type: The ICC SDG Type for this SDG
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sDGType")]
		public virtual string SDgtype => this.Value<string>(_publishedValueFallback, "sDGType");

		///<summary>
		/// Body Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("bodyContent")]
		public virtual global::Umbraco.Cms.Core.Models.Blocks.BlockGridModel BodyContent => global::Umbraco.Cms.Web.Common.PublishedModels.ContentArea.GetBodyContent(this, _publishedValueFallback);

		///<summary>
		/// Main Navigation Display Text: Text to appear in the main navigation. If unset, the page's main title will be used by default
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("mainNavigationDisplayText")]
		public virtual string MainNavigationDisplayText => global::Umbraco.Cms.Web.Common.PublishedModels.PageMetadata.GetMainNavigationDisplayText(this, _publishedValueFallback);

		///<summary>
		/// Page Title: the title of the page as displayed on the front-end
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("pageTitle")]
		public virtual string PageTitle => global::Umbraco.Cms.Web.Common.PublishedModels.PageMetadata.GetPageTitle(this, _publishedValueFallback);

		///<summary>
		/// SEO Description: The meta SEO description tag
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sEODescription")]
		public virtual string SEodescription => global::Umbraco.Cms.Web.Common.PublishedModels.PageMetadata.GetSEodescription(this, _publishedValueFallback);

		///<summary>
		/// SEO Title: The SEO title of the page (uses the Page Title property if unset)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sEOTitle")]
		public virtual string SEotitle => global::Umbraco.Cms.Web.Common.PublishedModels.PageMetadata.GetSEotitle(this, _publishedValueFallback);

		///<summary>
		/// Show In Main Navigation: If set to true, the page will appear in the main navigation for the site
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "13.0.0+9dfb300")]
		[ImplementPropertyType("showInMainNavigation")]
		public virtual bool ShowInMainNavigation => global::Umbraco.Cms.Web.Common.PublishedModels.PageMetadata.GetShowInMainNavigation(this, _publishedValueFallback);
	}
}
