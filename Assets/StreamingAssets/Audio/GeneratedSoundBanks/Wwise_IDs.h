/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMB_LIGHTBEAM = 1884576663U;
        static const AkUniqueID CHURCH3DEVENTS = 1376062894U;
        static const AkUniqueID CHURCH_ALTER_AMBIENCE = 1568742100U;
        static const AkUniqueID CHURCH_AMBISONIC = 1132321458U;
        static const AkUniqueID CRAWLSFX = 3690987655U;
        static const AkUniqueID DOOROPEN = 1404805401U;
        static const AkUniqueID DUNGEONAMB = 3254361931U;
        static const AkUniqueID DUNGEONSCREAM = 2115347266U;
        static const AkUniqueID ENDEVENT = 228553356U;
        static const AkUniqueID FOOTSTEP = 1866025847U;
        static const AkUniqueID INTROEVENT = 2271674691U;
        static const AkUniqueID LANTEREN_CHARGING = 3651057560U;
        static const AkUniqueID LANTEREN_DEPLOY_LOOP = 759350405U;
        static const AkUniqueID LANTEREN_DISCHARGE = 1818726255U;
        static const AkUniqueID LANTEREN_UNDEPLOY = 740769089U;
        static const AkUniqueID LIGHTBEAM = 1710262138U;
        static const AkUniqueID LOOSINGHEALTH = 938481918U;
        static const AkUniqueID LOWHEALTH = 1017222595U;
        static const AkUniqueID MASSIVEDEVICE = 3113871599U;
        static const AkUniqueID MENUMUSIC = 679636833U;
        static const AkUniqueID MENUMUSIC_STOP = 1115745636U;
        static const AkUniqueID MIRRORLIGHT = 1217934400U;
        static const AkUniqueID MIRRORLIGHT_OFF = 500177350U;
        static const AkUniqueID MIRRORTURN = 1903317937U;
        static const AkUniqueID MIRRORTURN_OFF = 3962630019U;
        static const AkUniqueID NOTLOOSINGHEALTH = 340196141U;
        static const AkUniqueID STOPALL = 3086540886U;
        static const AkUniqueID STOPALL_01 = 4256069028U;
        static const AkUniqueID TOWERAMB = 3134013836U;
        static const AkUniqueID VA_CLERK_PLAY = 1398273777U;
        static const AkUniqueID VA_PAUSE = 938429017U;
        static const AkUniqueID VA_RESUME = 662947992U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace ALTERAMB
        {
            static const AkUniqueID GROUP = 2677818465U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NOTPLAYING = 1227244058U;
                static const AkUniqueID PLAYING = 1852808225U;
            } // namespace STATE
        } // namespace ALTERAMB

        namespace DUNGEONAMB
        {
            static const AkUniqueID GROUP = 3254361931U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NOTPLAYING = 1227244058U;
                static const AkUniqueID PLAYING = 1852808225U;
            } // namespace STATE
        } // namespace DUNGEONAMB

        namespace ENDSTATE
        {
            static const AkUniqueID GROUP = 1596234923U;

            namespace STATE
            {
                static const AkUniqueID END = 529726532U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NOTEND = 2393614615U;
            } // namespace STATE
        } // namespace ENDSTATE

        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PAUSED = 319258907U;
                static const AkUniqueID UNPAUSED = 1365518790U;
            } // namespace STATE
        } // namespace GAMESTATE

        namespace HEALTH_STATE
        {
            static const AkUniqueID GROUP = 1264560589U;

            namespace STATE
            {
                static const AkUniqueID LOOSINGSANITY = 2627361338U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NOTLOOSINGSANITY = 3358664337U;
            } // namespace STATE
        } // namespace HEALTH_STATE

        namespace HPLOSS
        {
            static const AkUniqueID GROUP = 133846688U;

            namespace STATE
            {
                static const AkUniqueID DISABLED = 3248502869U;
                static const AkUniqueID ENABLED = 4174102348U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace HPLOSS

    } // namespace STATES

    namespace SWITCHES
    {
        namespace PLAYER_FOOTSTEPS
        {
            static const AkUniqueID GROUP = 1730208058U;

            namespace SWITCH
            {
                static const AkUniqueID CLOTH = 1367384683U;
                static const AkUniqueID METAL = 2473969246U;
                static const AkUniqueID STONE = 1216965916U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace PLAYER_FOOTSTEPS

        namespace VA_CLERK_SWITCH_GROUP
        {
            static const AkUniqueID GROUP = 232330383U;

            namespace SWITCH
            {
                static const AkUniqueID BEFOREDEPARTING = 2645581302U;
                static const AkUniqueID CLERKCHATTER1 = 711493436U;
                static const AkUniqueID CLERKCHATTER2 = 711493439U;
                static const AkUniqueID CLERKCHATTER3 = 711493438U;
                static const AkUniqueID CLERKCRAWL = 2986066245U;
                static const AkUniqueID CLERKINTRO = 1202250874U;
                static const AkUniqueID DUNGEONCHATTER = 994602288U;
                static const AkUniqueID INTRO = 1125500713U;
                static const AkUniqueID SECONDENCOUNTER = 1703442692U;
                static const AkUniqueID THIRDENCOUNTER = 3682658335U;
                static const AkUniqueID TOWER_DISSAPOINTED = 4085445222U;
                static const AkUniqueID TOWER_FOLLOWTHELIGHT = 2318992283U;
                static const AkUniqueID TOWER_LETTHELIGHTGUIDE_GRIMLY = 1337305014U;
                static const AkUniqueID TOWER_PRAYER = 317582866U;
                static const AkUniqueID TOWER_PROUD = 3419523551U;
                static const AkUniqueID TOWER_STEPINTOTHELIGHT_FINALE = 2903908110U;
                static const AkUniqueID TOWER_THECHURCHISALMOSTRESTORED = 2889570721U;
                static const AkUniqueID TOWER_YESTHELIGHTGROWSSTRONGER = 2448125293U;
            } // namespace SWITCH
        } // namespace VA_CLERK_SWITCH_GROUP

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID PLAYERHEALTH = 151362964U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID NEW_TRIGGER = 4163741908U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINS = 1764570813U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID _2D = 527871411U;
        static const AkUniqueID AMBISONICBUS = 4258699522U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID ROOMS = 1359360203U;
        static const AkUniqueID ROOMS_DOCKING = 532133889U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID CHURCH = 2568407992U;
        static const AkUniqueID CHURCH_01 = 741553742U;
        static const AkUniqueID OUTDOORS = 2730119150U;
        static const AkUniqueID OUTDOORS_01 = 2030475388U;
        static const AkUniqueID SMALLROOM = 2933838247U;
        static const AkUniqueID SMALLROOM_01 = 2743004395U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
