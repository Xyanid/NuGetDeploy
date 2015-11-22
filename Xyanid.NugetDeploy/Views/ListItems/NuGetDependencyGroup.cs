using System;
using Xyanid.Winforms.Selection;

namespace Xyanid.VisualStudioExtension.NuGetDeploy.Views.ListItems
{
	public partial class NuGetDependencyGroup : SelectableControl
	{
		#region Properties

		/// <summary>
		/// determines the group hosted by this class
		/// </summary>
		public Xml.NuGet.NuSpec.Group Group { get; private set; }

		/// <summary>
		/// action to be called then a dependency has been removed from this group
		/// </summary>
		public Action<NuGetDependency, NuGetDependencyGroup> OnDependencyRemoved { get; set; }

		#endregion

		#region Constructor

		public NuGetDependencyGroup()
		{
			InitializeComponent();
		}

		public NuGetDependencyGroup(Xml.NuGet.NuSpec.Group dependencyGroup) : this()
		{
			Group = dependencyGroup;

			_uiTargetFramework.Text = Group.TargetFramework;

			foreach (Xml.NuGet.NuSpec.Dependency dependency in dependencyGroup.Dependencies)
			{
				_uiDependencies.Items.Add(new NuGetDependency(dependency)
				{
					OnRemoveFromDependencyGroup = OnDependencyRemovedFromGroup
				});
			}

			_uiItemsCount.Text = _uiDependencies.Items.Count.ToString();

			Refresh();
		}

		#endregion

		#region Public

		/// <summary>
		/// adds the given nuget dependency to the list and refresh the control
		/// </summary>
		/// <param name="dependency">dependency to add</param>
		public void AddDependency(NuGetDependency dependency)
		{
			Group.Dependencies.Add(dependency.Dependency);

			dependency.OnRemoveFromDependencyGroup = OnDependencyRemovedFromGroup;

			_uiDependencies.Items.Add(dependency);

			_uiItemsCount.Text = _uiDependencies.Items.Count.ToString();

			Refresh();
		}

		/// <summary>
		/// removes the given dependency from the list if it exists
		/// </summary>
		/// <param name="dependency">dependency to remove</param>
		/// <returns>true if the dependency was removed</returns>
		public bool RemoveDependency(NuGetDependency dependency)
		{
			return RemoveDependencyById(dependency.Dependency.Id);
		}

		/// <summary>
		/// removes the given dependency from the list if it exists
		/// </summary>
		/// <param name="id">dependency to remove</param>
		/// <returns>true if the dependency was removed</returns>
		public bool RemoveDependencyById(string id)
		{
			for (int i = 0; i < _uiDependencies.Items.Count; i++)
				if (((NuGetDependency)_uiDependencies.Items[i]).Dependency.Id == id)
				{
					((NuGetDependency)_uiDependencies.Items[i]).OnRemoveFromDependencyGroup = null;

					Group.Dependencies.RemoveAt(i);

					_uiDependencies.Items.Remove(_uiDependencies.Items[i]);

					_uiItemsCount.Text = _uiDependencies.Items.Count.ToString();

					Refresh();

					return true;
				}

			return false;
		}

		#endregion

		#region Private

		/// <summary>
		/// will be called when a nugetDependency switched its targetframework
		/// </summary>
		/// <param name="dependency"></param>
		private void OnDependencyRemovedFromGroup(NuGetDependency dependency)
		{
			RemoveDependency(dependency);

			if (OnDependencyRemoved != null)
				OnDependencyRemoved(dependency, this);
		}

		#endregion
	}
}
