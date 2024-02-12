namespace NewRelicAlerts
{
    public static class NewRelicAlertsGraphQLQueries
    {
        public const string AlertsConditionDelete = @"
    mutation alertsConditionDelete ($accountId: Int!, $id: ID!) {
        alertsConditionDelete (accountId: $accountId, id: $id) {
            id
        }
    }";
    
        public const string AlertsNotificationChannelsAddToPolicy = @"
    mutation alertsNotificationChannelsAddToPolicy ($accountId: Int!, $notificationChannelIds: [ID!]!, $policyId: ID!) {
        alertsNotificationChannelsAddToPolicy (accountId: $accountId, notificationChannelIds: $notificationChannelIds, policyId: $policyId) {
            errors {
                description
                errorType
                notificationChannelId
            }
            notificationChannels {
                id
            }
            policyId
        }
    }";
    
        public const string AlertsMutingRuleCreate = @"
    mutation alertsMutingRuleCreate ($accountId: Int!, $rule: AlertsMutingRuleInput!) {
        alertsMutingRuleCreate (accountId: $accountId, rule: $rule) {
            accountId
            condition {
                conditions {
                    attribute
                    operator
                    values
                }
                operator
            }
            createdAt
            createdBy
            createdByUser {
                email
                gravatar
                id
                name
            }
            description
            enabled
            id
            name
            schedule {
                endRepeat
                endTime
                nextEndTime
                nextStartTime
                repeat
                repeatCount
                startTime
                timeZone
                weeklyRepeatDays
            }
            status
            updatedAt
            updatedBy
            updatedByUser {
                email
                gravatar
                id
                name
            }
        }
    }";
    
        public const string AlertsMutingRuleDelete = @"
    mutation alertsMutingRuleDelete ($accountId: Int!, $id: ID!) {
        alertsMutingRuleDelete (accountId: $accountId, id: $id) {
            id
        }
    }";
    
        public const string AlertsMutingRuleUpdate = @"
    mutation alertsMutingRuleUpdate ($accountId: Int!, $id: ID!, $rule: AlertsMutingRuleUpdateInput!) {
        alertsMutingRuleUpdate (accountId: $accountId, id: $id, rule: $rule) {
            accountId
            condition {
                conditions {
                    attribute
                    operator
                    values
                }
                operator
            }
            createdAt
            createdBy
            createdByUser {
                email
                gravatar
                id
                name
            }
            description
            enabled
            id
            name
            schedule {
                endRepeat
                endTime
                nextEndTime
                nextStartTime
                repeat
                repeatCount
                startTime
                timeZone
                weeklyRepeatDays
            }
            status
            updatedAt
            updatedBy
            updatedByUser {
                email
                gravatar
                id
                name
            }
        }
    }";
    
        public const string AlertsNotificationChannelCreate = @"
    mutation alertsNotificationChannelCreate ($accountId: Int!, $notificationChannel: AlertsNotificationChannelCreateConfiguration!) {
        alertsNotificationChannelCreate (accountId: $accountId, notificationChannel: $notificationChannel) {
            error {
                description
                errorType
            }
            notificationChannel
        }
    }";
    
        public const string AlertsNrqlConditionBaselineUpdate = @"
    mutation alertsNrqlConditionBaselineUpdate ($accountId: Int!, $condition: AlertsNrqlConditionUpdateBaselineInput!, $id: ID!) {
        alertsNrqlConditionBaselineUpdate (accountId: $accountId, condition: $condition, id: $id) {
            baselineDirection
            description
            enabled
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsNrqlConditionOutlierUpdate = @"
    mutation alertsNrqlConditionOutlierUpdate ($accountId: Int!, $condition: AlertsNrqlConditionUpdateOutlierInput!, $id: ID!) {
        alertsNrqlConditionOutlierUpdate (accountId: $accountId, condition: $condition, id: $id) {
            description
            enabled
            expectedGroups
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            openViolationOnGroupOverlap
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsNrqlConditionOutlierCreate = @"
    mutation alertsNrqlConditionOutlierCreate ($accountId: Int!, $condition: AlertsNrqlConditionOutlierInput!, $policyId: ID!) {
        alertsNrqlConditionOutlierCreate (accountId: $accountId, condition: $condition, policyId: $policyId) {
            description
            enabled
            expectedGroups
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            openViolationOnGroupOverlap
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsNrqlConditionBaselineCreate = @"
    mutation alertsNrqlConditionBaselineCreate ($accountId: Int!, $condition: AlertsNrqlConditionBaselineInput!, $policyId: ID!) {
        alertsNrqlConditionBaselineCreate (accountId: $accountId, condition: $condition, policyId: $policyId) {
            baselineDirection
            description
            enabled
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsNotificationChannelsRemoveFromPolicy = @"
    mutation alertsNotificationChannelsRemoveFromPolicy ($accountId: Int!, $notificationChannelIds: [ID!]!, $policyId: ID!) {
        alertsNotificationChannelsRemoveFromPolicy (accountId: $accountId, notificationChannelIds: $notificationChannelIds, policyId: $policyId) {
            errors {
                description
                errorType
                notificationChannelId
            }
            notificationChannels {
                id
            }
            policyId
        }
    }";
    
        public const string AlertsNrqlConditionStaticUpdate = @"
    mutation alertsNrqlConditionStaticUpdate ($accountId: Int!, $condition: AlertsNrqlConditionUpdateStaticInput!, $id: ID!) {
        alertsNrqlConditionStaticUpdate (accountId: $accountId, condition: $condition, id: $id) {
            description
            enabled
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            valueFunction
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsPolicyCreate = @"
    mutation alertsPolicyCreate ($accountId: Int!, $policy: AlertsPolicyInput!) {
        alertsPolicyCreate (accountId: $accountId, policy: $policy) {
            accountId
            id
            incidentPreference
            name
        }
    }";
    
        public const string AlertsNrqlConditionStaticCreate = @"
    mutation alertsNrqlConditionStaticCreate ($accountId: Int!, $condition: AlertsNrqlConditionStaticInput!, $policyId: ID!) {
        alertsNrqlConditionStaticCreate (accountId: $accountId, condition: $condition, policyId: $policyId) {
            description
            enabled
            expiration {
                closeViolationsOnExpiration
                expirationDuration
                openViolationOnExpiration
            }
            id
            name
            nrql {
                evaluationOffset
                query
            }
            policyId
            runbookUrl
            signal {
                aggregationDelay
                aggregationMethod
                aggregationTimer
                aggregationWindow
                evaluationOffset
                fillOption
                fillValue
                slideBy
            }
            terms {
                operator
                priority
                threshold
                thresholdDuration
                thresholdOccurrences
            }
            type
            valueFunction
            violationTimeLimit
            violationTimeLimitSeconds
        }
    }";
    
        public const string AlertsPolicyUpdate = @"
    mutation alertsPolicyUpdate ($accountId: Int!, $id: ID!, $policy: AlertsPolicyUpdateInput!) {
        alertsPolicyUpdate (accountId: $accountId, id: $id, policy: $policy) {
            accountId
            id
            incidentPreference
            name
        }
    }";
    
        public const string AlertsPolicyDelete = @"
    mutation alertsPolicyDelete ($accountId: Int!, $id: ID!) {
        alertsPolicyDelete (accountId: $accountId, id: $id) {
            id
        }
    }";
    
        public const string AlertsNotificationChannelDelete = @"
    mutation alertsNotificationChannelDelete ($accountId: Int!, $id: ID!) {
        alertsNotificationChannelDelete (accountId: $accountId, id: $id) {
            error {
                description
                errorType
                notificationChannelId
            }
            id
        }
    }";
    
        public const string AlertsNotificationChannelUpdate = @"
    mutation alertsNotificationChannelUpdate ($accountId: Int!, $id: ID!, $notificationChannel: AlertsNotificationChannelUpdateConfiguration!) {
        alertsNotificationChannelUpdate (accountId: $accountId, id: $id, notificationChannel: $notificationChannel) {
            error {
                description
                errorType
                notificationChannelId
            }
            notificationChannel
        }
    }";
    }
}