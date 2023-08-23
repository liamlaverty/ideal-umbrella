//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v12.0.1+20a4e47
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
	// Mixin Content Type with alias "geolocation"
	/// <summary>Entity Geolocation</summary>
	public partial interface IGeolocation : IPublishedElement
	{
		/// <summary>Latitude</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string Latitude { get; }

		/// <summary>Longitude</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string Longitude { get; }

		/// <summary>Map Zoom</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		int MapZoom { get; }

		/// <summary>Place Name</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string PlaceName { get; }
	}

	/// <summary>Entity Geolocation</summary>
	[PublishedModel("geolocation")]
	public partial class Geolocation : PublishedElementModel, IGeolocation
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		public new const string ModelTypeAlias = "geolocation";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Geolocation, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Geolocation(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Latitude: The geocoord latitude for the location
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("latitude")]
		public virtual string Latitude => GetLatitude(this, _publishedValueFallback);

		/// <summary>Static getter for Latitude</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetLatitude(IGeolocation that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "latitude");

		///<summary>
		/// Longitude: The geocoord longitude for the location
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("longitude")]
		public virtual string Longitude => GetLongitude(this, _publishedValueFallback);

		/// <summary>Static getter for Longitude</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetLongitude(IGeolocation that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "longitude");

		///<summary>
		/// Map Zoom: A mapzoom for this entity, 1 is further away, 15 is very close
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[ImplementPropertyType("mapZoom")]
		public virtual int MapZoom => GetMapZoom(this, _publishedValueFallback);

		/// <summary>Static getter for Map Zoom</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		public static int GetMapZoom(IGeolocation that, IPublishedValueFallback publishedValueFallback) => that.Value<int>(publishedValueFallback, "mapZoom");

		///<summary>
		/// Place Name: A descriptive title for this place
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("placeName")]
		public virtual string PlaceName => GetPlaceName(this, _publishedValueFallback);

		/// <summary>Static getter for Place Name</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "12.0.1+20a4e47")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetPlaceName(IGeolocation that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "placeName");
	}
}