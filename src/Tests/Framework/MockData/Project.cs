﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	public class Project
	{
		public string Name { get; set; }
		public StateOfBeing State { get; set; }
		public DateTime StartedOn { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; }
		public IList<Tag> CuratedTags { get; set; }
		public Dictionary<string, Metadata> Metadata { get; set; }

		public static Faker<Project> Generator { get; } =
			new Faker<Project>()
				.RuleFor(p => p.Name, f => f.Person.Company.Name)
				.RuleFor(p => p.State, f => f.PickRandom<StateOfBeing>())
				.RuleFor(p => p.StartedOn, p => p.Date.Past())
				.RuleFor(p => p.LastActivity, p => p.Date.Recent())
				.RuleFor(p => p.LeadDeveloper, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Tags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 50)))
				.RuleFor(p => p.CuratedTags, f => Tag.Generator.Generate(Gimme.Random.Number(1, 5)).ToList())
			;

		public static IList<Project> Projects { get; } =
			Project.Generator.Generate(100).ToList();
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum StateOfBeing
	{
		BellyUp,
		Stable,
		VeryActive
	}


	public class Metadata
	{
		public DateTime Created { get; set; }
	}
}
