Select *
From CarModels as Model,
	 CarConfigurations as Config,
	 GroupDetails as Groups,
	 SubgroupDetails as Subgroup,
	 Details
Where Model.CarId = Config.CarId AND
	  Config.ConfigurationId = Groups.ConfigurationId AND
	  Groups.GroupId = Subgroup.GroupId AND
	  Subgroup.SubgroupId = Details.SubgroupId