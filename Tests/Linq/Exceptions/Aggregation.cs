﻿using System;
using System.Linq;

using NUnit.Framework;

namespace Tests.Exceptions
{
	[TestFixture]
	public class Aggregtion : TestBase
	{
		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void NonNullableMax1([DataContexts] string context)
		{
			using (var db = GetDataContext(context))
				db.Parent.Where(_ => _.ParentID < 0).Max(_ => _.ParentID);
		}

		[Test]
		public void NonNullableMax2([DataContexts] string context)
		{
			using (var db = GetDataContext(context))
			{
				var q =
					from p in db.Parent
					select new
					{
						max = p.Children.Where(_ => _.ParentID < 0).Max(_ => _.ParentID)
					};

				Assert.Catch<InvalidOperationException>(() => q.ToList());
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void NonNullableAverage([DataContexts] string context)
		{
			using (var db = GetDataContext(context))
				db.Parent.Where(_ => _.ParentID < 0).Average(_ => _.ParentID);
		}
	}
}
