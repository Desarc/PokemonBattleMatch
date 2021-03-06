﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Optimizer.Data {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Files {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Files() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Optimizer.Data.Files", typeof(Files).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to [
        ///	{ &quot;name&quot;: &quot;Acid&quot;, &quot;type&quot;: &quot;POISON&quot;, dps: 9.52 },
        ///	{ &quot;name&quot;: &quot;Bite&quot;, &quot;type&quot;: &quot;DARK&quot;, dps: 12 },
        ///	{ &quot;name&quot;: &quot;Bubble&quot;, &quot;type&quot;: &quot;WATER&quot;, dps: 10.87 },
        ///	{ &quot;name&quot;: &quot;Bug Bite&quot;, &quot;type&quot;: &quot;BUG&quot;, dps: 11.11 },
        ///	{ &quot;name&quot;: &quot;Bullet Punch&quot;, &quot;type&quot;: &quot;STEEL&quot;, dps: 8.33 },
        ///	{ &quot;name&quot;: &quot;Confusion&quot;, &quot;type&quot;: &quot;PSYCHIC&quot;, dps: 9.93 },
        ///	{ &quot;name&quot;: &quot;Cut&quot;, &quot;type&quot;: &quot;NORMAL&quot;, dps: 10.62 },
        ///	{ &quot;name&quot;: &quot;Dragon Breath&quot;, &quot;type&quot;: &quot;DRAGON&quot;, dps: 12 },
        ///	{ &quot;name&quot;: &quot;Ember&quot;, &quot;type&quot;: &quot;FIRE&quot;, dps: 9.52 },
        ///	{ &quot;name&quot;: &quot;Feint Attack&quot;, &quot;typ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FastMoves {
            get {
                return ResourceManager.GetString("FastMoves", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [
        ///	{ 
        ///		&quot;number&quot;: 1,
        ///		&quot;name&quot;: &quot;Bulbasaur&quot;, 
        ///		&quot;stage&quot;: 1,
        ///		&quot;maxStage&quot;: 3,
        ///		&quot;maxCP&quot;: 1071,
        ///		&quot;maxHP&quot;: 82,
        ///		&quot;firstType&quot;: &quot;GRASS&quot;,
        ///		&quot;secondType&quot;: &quot;POISON&quot;,
        ///		&quot;fastAttacks&quot;: [ &quot;Tackle&quot;, &quot;Vine Whip&quot; ],
        ///		&quot;specialAttacks&quot;: [ &quot;Sludge Bomb&quot;, &quot;Seed Bomb&quot;, &quot;Power Whip&quot; ]
        ///	},
        ///	{ 
        ///		&quot;number&quot;: 2,
        ///		&quot;name&quot;: &quot;Ivysaur&quot;, 
        ///		&quot;stage&quot;: 2,
        ///		&quot;maxStage&quot;: 3,
        ///		&quot;maxCP&quot;: 1632,
        ///		&quot;maxHP&quot;: 106,
        ///		&quot;firstType&quot;: &quot;GRASS&quot;,
        ///		&quot;secondType&quot;: &quot;POISON&quot;,
        ///		&quot;fastAttacks&quot;: [ &quot;Razor Leaf&quot;, &quot;Vine Whip&quot; ],
        ///		&quot;specialAttac [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PokemonTemplates {
            get {
                return ResourceManager.GetString("PokemonTemplates", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [
        ///	{ &quot;name&quot;: &quot;Aerial Ace&quot;, &quot;type&quot;: &quot;FLYING&quot;, dps: 10.34, criticalHitChance: 0.05 },
        ///	{ &quot;name&quot;: &quot;Air Cutter&quot;, &quot;type&quot;: &quot;FLYING&quot;, dps: 9.09, criticalHitChance: 0.25 },
        ///	{ &quot;name&quot;: &quot;Ancient Power&quot;, &quot;type&quot;: &quot;ROCK&quot;, dps: 9.72, criticalHitChance: 0.05 },
        ///	{ &quot;name&quot;: &quot;Aqua Jet&quot;, &quot;type&quot;: &quot;WATER&quot;, dps: 10.64, criticalHitChance: 0.05 },
        ///	{ &quot;name&quot;: &quot;Aqua Tail&quot;, &quot;type&quot;: &quot;WATER&quot;, dps: 19.15, criticalHitChance: 0.05 },
        ///	{ &quot;name&quot;: &quot;Blizzard&quot;, &quot;type&quot;: &quot;ICE&quot;, dps: 26.64, criticalHitChance: 0.05 },
        ///	{ &quot;name&quot;: &quot;Body Slam&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SpecialMoves {
            get {
                return ResourceManager.GetString("SpecialMoves", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [
        ///	{ 
        ///		&quot;attackingType&quot;: &quot;NORMAL&quot;,
        ///		&quot;typeModifiers&quot;: [ 
        ///			{ &quot;defendingType&quot;: &quot;NORMAL&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;FIRE&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;WATER&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;ELECTRIC&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;GRASS&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;ICE&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;FIGHTING&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;POISON&quot;, &quot;modifier&quot;: 1 },
        ///			{ &quot;defendingType&quot;: &quot;GROUND&quot;, &quot;modifier&quot;: 1 },
        ///			{ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TypeMathcups {
            get {
                return ResourceManager.GetString("TypeMathcups", resourceCulture);
            }
        }
    }
}
