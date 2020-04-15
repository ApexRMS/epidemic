// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    static class Shared
    {
        //Common
        public const string ITERATION_COLUMN_NAME = "Iteration";
        public const string TIMESTEP_COLUMN_NAME = "Timestep";
        public const string DATE_COLUMN_NAME = "Date";
        public const string JURISDICTION_COLUMN_NAME = "Jurisdiction";
        public const string VALUE_COLUMN_NAME = "Value";
        public const string DATASHEET_NAME_COLUMN_NAME = "Name";
        public const string CUMULATIVE_VALUE_COLUMN_NAME = "CumulativeValue";
        public const string DISTRIBUTION_TYPE_COLUMN_NAME = "DistributionType";
        public const string DISTRIBUTION_FREQUENCY_COLUMN_NAME = "DistributionFrequencyID";
        public const string DISTRIBUTIONSD_COLUMN_NAME = "DistributionSD";
        public const string DISTRIBUTIONMIN_COLUMN_NAME = "DistributionMin";
        public const string DISTRIBUTIONMAX_COLUMN_NAME = "DistributionMax";

        //Jurisdiction
        public const string DATASHEET_JURISDICTION_NAME = "epidemic_Jurisdiction";
        public const string DATASHEET_JURISDICTION_ID_COLUMN_NAME = "JurisdictionID";

        //Terminology
        public const string DATASHEET_TERMINOLOGY_NAME = "epidemic_Terminology";
        public const string DATASHEET_TERMINOLOGY_JURISDICTION_LABEL_COLUMN_NAME = "JurisdictionLabel";

        //Distribution Type
        public const string DISTRIBUTION_TYPE_DATASHEET_NAME = "corestime_DistributionType";
        public const string DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME = "IsInternal";
        public const string DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER = "Uniform Integer";

        //Run Control
        public const string DATASHEET_RUN_CONTROL_NAME = "epidemic_RunControl";
        public const string DATASHEET_RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME = "MinimumTimestep";
        public const string DATASHEET_RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME = "MaximumTimestep";
        public const string DATASHEET_RUN_CONTROL_MIN_ITERATION_COLUMN_NAME = "MinimumIteration";
        public const string DATASHEET_RUN_CONTROL_MAX_ITERATION_COLUMN_NAME = "MaximumIteration";
        public const string DATASHEET_RUN_CONTROL_MODEL_HISTORICAL_DEATHS_COLUMN_NAME = "ModelHistoricalDeaths";
        public const string DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME = "StartDate";
        public const string DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME = "EndDate";

        //Runtime Jurisdiction
        public const string DATASHEET_RUNTIME_JURISDICTION_NAME = "epidemic_RuntimeJurisdiction";

        //Population
        public const string DATASHEET_POPULATION_NAME = "epidemic_Population";
        public const string DATASHEET_POPULATION_TOTAL_SIZE_COLUMN_NAME = "TotalSize";

        //Actual Deaths
        public const string  DATASHEET_ACTUAL_DEATH_NAME = "epidemic_ActualDeath";

        //Growth Rate
        public const string  DATASHEET_GROWTH_RATE_NAME = "epidemic_GrowthRate";

        //Fatality Rate
        public const string DATASHEET_FATALITY_RATE_NAME = "epidemic_FatalityRate";

        //Attack Rate
        public const string  DATASHEET_ATTACK_RATE_NAME = "epidemic_AttackRate";

        //Model Type
        public const string  DATASHEET_MODEL_TYPE_NAME = "epidemic_ModelType";
        public const string  DATASHEET_MODEL_TYPE_COLUMN_NAME = "ModelType";

        //Incubation Period
        public const string  DATASHEET_INCUBATION_PERIOD_NAME = "epidemic_IncubationPeriod";

        //Symptom Period
        public const string DATASHEET_SYMPTOM_PERIOD_NAME = "epidemic_SymptomPeriod";

        //Distribution Value
        public const string DISTRIBUTION_VALUE_DATASHEET_NAME = "epidemic_DistributionValue";
        public const string DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME = "DistributionTypeID";
        public const string DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME = "ExternalVariableTypeID";
        public const string DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME = "ExternalVariableMin";
        public const string DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME = "ExternalVariableMax";
        public const string DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME = "Value";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME = "ValueDistributionTypeID";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME = "ValueDistributionFrequency";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME = "ValueDistributionSD";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME = "ValueDistributionMin";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME = "ValueDistributionMax";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME = "ValueDistributionRelativeFrequency";

        //Output
        public const string DATASHEET_OUTPUT_INCUBATION_PERIOD_NAME = "epidemic_OutputIncubationPeriod";
        public const string DATASHEET_OUTPUT_SYMPTOM_PERIOD_NAME = "epidemic_OutputSymptomPeriod";
        public const string DATASHEET_OUTPUT_INFECTED_PERIOD_NAME = "epidemic_OutputInfectedPeriod";
        public const string DATASHEET_OUTPUT_GROWTH_RATE_NAME = "epidemic_OutputGrowthRate";
        public const string DATASHEET_OUTPUT_FATALITY_RATE_NAME = "epidemic_OutputFatalityRate";
        public const string DATASHEET_OUTPUT_ATTACK_RATE_NAME = "epidemic_OutputAttackRate";
        public const string DATASHEET_OUTPUT_INFECTED_NAME = "epidemic_OutputInfected";
        public const string DATASHEET_OUTPUT_DEATH_NAME = "epidemic_OutputDeath";

        public static void ThrowEpidemicException(string message)
        {
            ThrowEpidemicException(message, new object[0]);
        }

        public static void ThrowEpidemicException(string message, params object[] args)
        {
            throw new Exception(string.Format(CultureInfo.InvariantCulture, message, args));
        }

        public static bool GetNullableBool(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        public static int? GetNullableInt(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
        }

        public static double? GetNullableDouble(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        public static string GetJurisdictionLabel(DataSheet terminologyDataSheet)
        {
            DataRow dr = terminologyDataSheet.GetDataRow();
            string JurisdictionLabel = "Jurisdiction";

            if (dr != null)
            {
                if (dr[Shared.DATASHEET_TERMINOLOGY_JURISDICTION_LABEL_COLUMN_NAME] != DBNull.Value)
                {
                    JurisdictionLabel = Convert.ToString(
                        dr[Shared.DATASHEET_TERMINOLOGY_JURISDICTION_LABEL_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);
                }
            }

            return JurisdictionLabel;
        }
    }
}
