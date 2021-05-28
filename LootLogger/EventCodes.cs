﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootLogger
{
    public enum LootLogEventCodes : int
    {
        evOtherGrabbedLoot = 251,
    }
   public enum EventCodes
    {
        evevLeave = 1,
        evJoinFinished,
        evMove,
        evTeleport,
        evChangeEquipment,
        evHealthUpdate,
        evEnergyUpdate,
        evDamageShieldUpdate,
        evCraftingFocusUpdate,
        evActiveSpellEffectsUpdate,
        evResetCooldowns,
        evAttack,
        evCastStart,
        evCastCancel,
        evCastTimeUpdate,
        evCastFinished,
        evCastSpell,
        evCastHit,
        evCastHits,
        evChannelingEnded,
        evAttackBuilding,
        evInventoryPutItem,
        evInventoryDeleteItem,
        evNewCharacter,
        evNewEquipmentItem,
        evNewSimpleItem,
        evNewFurnitureItem,
        evNewJournalItem,
        evNewSimpleHarvestableObject,
        evNewSimpleHarvestableObjectList,
        evNewHarvestableObject,
        evNewSilverObject,
        evNewBuilding,
        evHarvestableChangeState,
        evMobChangeState,
        evFactionBuildingInfo,
        evCraftBuildingInfo,
        evRepairBuildingInfo,
        evMeldBuildingInfo,
        evConstructionSiteInfo,
        evPlayerBuildingInfo,
        evFarmBuildingInfo,
        evTutorialBuildingInfo,
        evLaborerObjectInfo,
        evLaborerObjectJobInfo,
        evMarketPlaceBuildingInfo,
        evHarvestStart,
        evHarvestCancel,
        evHarvestFinished,
        evTakeSilver,
        evActionOnBuildingStart,
        evActionOnBuildingCancel,
        evActionOnBuildingFinished,
        evItemRerollQualityStart,
        evItemRerollQualityCancel,
        evItemRerollQualityFinished,
        evInstallResourceStart,
        evInstallResourceCancel,
        evInstallResourceFinished,
        evCraftItemFinished,
        evLogoutCancel,
        evChatMessage,
        evChatSay,
        evChatWhisper,
        evChatMuted,
        evPlayEmote,
        evStopEmote,
        evSystemMessage,
        evUtilityTextMessage,
        evUpdateMoney,
        evUpdateFame,
        evUpdateLearningPoints,
        evUpdateReSpecPoints,
        evUpdateCurrency,
        evUpdateFactionStanding,
        evRespawn,
        evServerDebugLog,
        evCharacterEquipmentChanged,
        evRegenerationHealthChanged,
        evRegenerationEnergyChanged,
        evRegenerationMountHealthChanged,
        evRegenerationCraftingChanged,
        evRegenerationHealthEnergyComboChanged,
        evRegenerationPlayerComboChanged,
        evDurabilityChanged,
        evNewLoot,
        evAttachItemContainer,
        evDetachItemContainer,
        evGuildVaultInfo,
        evGuildUpdate,
        evGuildPlayerUpdated,
        evInvitedToGuild,
        evGuildMemberWorldUpdate,
        evUpdateMatchDetails,
        evObjectEvent,
        evNewMonolithObject,
        evNewSiegeCampObject,
        evNewOrbObject,
        evNewCastleObject,
        evNewSpellEffectArea,
        evNewChainSpell,
        evUpdateChainSpell,
        evNewTreasureChest,
        evStartMatch,
        evStartTerritoryMatchInfos,
        evStartArenaMatchInfos,
        evEndTerritoryMatch,
        evEndArenaMatch,
        evMatchUpdate,
        evActiveMatchUpdate,
        evNewMob,
        evDebugAggroInfo,
        evDebugVariablesInfo,
        evDebugReputationInfo,
        evDebugDiminishingReturnInfo,
        evClaimOrbStart,
        evClaimOrbFinished,
        evClaimOrbCancel,
        evOrbUpdate,
        evOrbClaimed,
        evNewWarCampObject,
        evNewMatchLootChestObject,
        evNewArenaExit,
        evGuildMemberTerritoryUpdate,
        evInvitedMercenaryToMatch,
        evClusterInfoUpdate,
        evForcedMovement,
        evForcedMovementCancel,
        evCharacterStats,
        evCharacterStatsKillHistory,
        evCharacterStatsDeathHistory,
        evGuildStats,
        evKillHistoryDetails,
        evFullAchievementInfo,
        evFinishedAchievement,
        evAchievementProgressInfo,
        evFullAchievementProgressInfo,
        evFullTrackedAchievementInfo,
        evFullAutoLearnAchievementInfo,
        evQuestGiverQuestOffered,
        evQuestGiverDebugInfo,
        evConsoleEvent,
        evTimeSync,
        evChangeAvatar,
        evChangeMountSkin,
        evGameEvent,
        evKilledPlayer,
        evDied,
        evKnockedDown,
        evMatchPlayerJoinedEvent,
        evMatchPlayerStatsEvent,
        evMatchPlayerStatsCompleteEvent,
        evMatchTimeLineEventEvent,
        evMatchPlayerMainGearStatsEvent,
        evMatchPlayerChangedAvatarEvent,
        evInvitationPlayerTrade,
        evPlayerTradeStart,
        evPlayerTradeCancel,
        evPlayerTradeUpdate,
        evPlayerTradeFinished,
        evPlayerTradeAcceptChange,
        evMiniMapPing,
        evMarketPlaceNotification,
        evDuellingChallengePlayer,
        evNewDuellingPost,
        evDuelStarted,
        evDuelEnded,
        evDuelDenied,
        evDuelLeftArea,
        evDuelReEnteredArea,
        evNewRealEstate,
        evMiniMapOwnedBuildingsPositions,
        evRealEstateListUpdate,
        evGuildLogoUpdate,
        evGuildLogoChanged,
        evPlaceableItemPlace,
        evPlaceableItemPlaceCancel,
        evFurnitureObjectBuffProviderInfo,
        evFurnitureObjectCheatProviderInfo,
        evFarmableObjectInfo,
        evLaborerObjectPlace,
        evLaborerObjectPlaceCancel,
        evNewUnreadMails,
        evGuildLogoObjectUpdate,
        evStartLogout,
        evNewChatChannels,
        evJoinedChatChannel,
        evLeftChatChannel,
        evRemovedChatChannel,
        evAccessStatus,
        evMounted,
        evMountCancel,
        evNewTravelpoint,
        evNewIslandAccessPoint,
        evNewExit,
        evUpdateHome,
        evUpdateChatSettings,
        evResurrectionOffer,
        evResurrectionReply,
        evLootEquipmentChanged,
        evUpdateUnlockedGuildLogos,
        evUpdateUnlockedAvatars,
        evUpdateUnlockedAvatarRings,
        evUpdateUnlockedBuildings,
        evDailyLoginBonus,
        evNewIslandManagement,
        evNewTeleportStone,
        evCloak,
        evPartyInvitation,
        evPartyJoined,
        evPartyDisbanded,
        evPartyPlayerJoined,
        evPartyChangedOrder,
        evPartyPlayerLeft,
        evPartyLeaderChanged,
        evPartyLootSettingChangedPlayer,
        evPartySilverGained,
        evPartyPlayerUpdated,
        evPartyInvitationPlayerBusy,
        evPartyMarkedObjectsUpdated,
        evPartyOnClusterPartyJoined,
        evPartySetRoleFlag,
        evSpellCooldownUpdate,
        evNewHellgate,
        evNewHellgateExit,
        evNewExpeditionExit,
        evNewExpeditionNarrator,
        evExitEnterStart,
        evExitEnterCancel,
        evExitEnterFinished,
        evHellClusterTimeUpdate,
        evNewQuestGiverObject,
        evFullQuestInfo,
        evQuestProgressInfo,
        evQuestGiverInfoForPlayer,
        evFullExpeditionInfo,
        evExpeditionQuestProgressInfo,
        evInvitedToExpedition,
        evExpeditionRegistrationInfo,
        evEnteringExpeditionStart,
        evEnteringExpeditionCancel,
        evRewardGranted,
        evArenaRegistrationInfo,
        evEnteringArenaStart,
        evEnteringArenaCancel,
        evEnteringArenaLockStart,
        evEnteringArenaLockCancel,
        evInvitedToArenaMatch,
        evPlayerCounts,
        evInCombatStateUpdate,
        evOtherGrabbedLoot,
        evSiegeCampClaimStart,
        evSiegeCampClaimCancel,
        evSiegeCampClaimFinished,
        evSiegeCampScheduleResult,
        evTreasureChestUsingStart,
        evTreasureChestUsingFinished,
        evTreasureChestUsingCancel,
        evTreasureChestUsingOpeningComplete,
        evTreasureChestForceCloseInventory,
        evPremiumChanged,
        evPremiumExtended,
        evPremiumLifeTimeRewardGained,
        evLaborerGotUpgraded,
        evJournalGotFull,
        evJournalFillError,
        evFriendRequest,
        evFriendRequestInfos,
        evFriendInfos,
        evFriendRequestAnswered,
        evFriendOnlineStatus,
        evFriendRequestCanceled,
        evFriendRemoved,
        evFriendUpdated,
        evPartyLootItems,
        evPartyLootItemsRemoved,
        evReputationUpdate,
        evDefenseUnitAttackBegin,
        evDefenseUnitAttackEnd,
        evDefenseUnitAttackDamage,
        evUnrestrictedPvpZoneUpdate,
        evReputationImplicationUpdate,
        evNewMountObject,
        evMountHealthUpdate,
        evMountCooldownUpdate,
        evNewExpeditionAgent,
        evNewExpeditionCheckPoint,
        evExpeditionStartEvent,
        evVoteEvent,
        evRatingEvent,
        evNewArenaAgent,
        evBoostFarmable,
        evUseFunction,
        evNewPortalEntrance,
        evNewPortalExit,
        evNewRandomDungeonExit,
        evWaitingQueueUpdate,
        evPlayerMovementRateUpdate,
        evObserveStart,
        evMinimapZergs,
        evPaymentTransactions,
        evPerformanceStatsUpdate,
        evOverloadModeUpdate,
        evDebugDrawEvent,
        evRecordCameraMove,
        evRecordStart,
        evTerritoryClaimStart,
        evTerritoryClaimCancel,
        evTerritoryClaimFinished,
        evTerritoryScheduleResult,
        evUpdateAccountState,
        evStartDeterministicRoam,
        evGuildFullAccessTagsUpdated,
        evGuildAccessTagUpdated,
        evGvgSeasonUpdate,
        evGvgSeasonCheatCommand,
        evSeasonPointsByKillingBooster,
        evFishingStart,
        evFishingCast,
        evFishingCatch,
        evFishingFinished,
        evFishingCancel,
        evNewFloatObject,
        evNewFishingZoneObject,
        evFishingMiniGame,
        evSteamAchievementCompleted,
        evUpdatePuppet,
        evChangeFlaggingFinished,
        evNewOutpostObject,
        evOutpostUpdate,
        evOutpostClaimed,
        evOutpostReward,
        evOverChargeEnd,
        evOverChargeStatus,
        evPartyFinderFullUpdate,
        evPartyFinderUpdate,
        evPartyFinderApplicantsUpdate,
        evPartyFinderEquipmentSnapshot,
        evPartyFinderJoinRequestDeclined,
        evNewUnlockedPersonalSeasonRewards,
        evPersonalSeasonPointsGained,
        evEasyAntiCheatMessageToClient,
        evMatchLootChestOpeningStart,
        evMatchLootChestOpeningFinished,
        evMatchLootChestOpeningCancel,
        evNotifyCrystalMatchReward,
        evCrystalRealmFeedback,
        evNewLocationMarker,
        evNewTutorialBlocker,
        evNewInformationProvider,
        evNewDynamicGuildLogo,
        evTutorialUpdate,
        evTriggerHintBox,
        evRandomDungeonPositionInfo,
        evNewLootChest,
        evUpdateLootChest,
        evLootChestOpened,
        evNewShrine,
        evUpdateShrine,
        evMutePlayerUpdate,
        evShopTileUpdate,
        evShopUpdate,
        evEasyAntiCheatKick,
        evUnlockVanityUnlock,
        evCustomizationChanged,
        evBankVaultInfo
    }
}
