﻿using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using sodoff.Attributes;
using sodoff.Model;
using sodoff.Schema;
using sodoff.Util;

namespace sodoff.Controllers.Common;
public class AchievementController : Controller {

    private readonly DBContext ctx;
    public AchievementController(DBContext ctx) {
        this.ctx = ctx;
    }

    [HttpPost]
    //[Produces("application/xml")]
    [Route("AchievementWebService.asmx/GetPetAchievementsByUserID")]
    public IActionResult GetPetAchievementsByUserID() {
        // TODO, this is a placeholder
        return Ok("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ArrayOfUserAchievementInfo xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://api.jumpstart.com/\" />");
    }

    [HttpPost]
    //[Produces("application/xml")]
    [Route("AchievementWebService.asmx/GetAllRanks")]
    public IActionResult GetAllRanks() {
        // TODO, this is a placeholder
        return Ok(XmlUtil.ReadResourceXmlString("allranks"));
    }

    [HttpPost]
    //[Produces("application/xml")]
    [Route("AchievementWebService.asmx/GetAchievementTaskInfo")]
    public IActionResult GetAchievementTaskInfo() {
        // TODO
        return Ok(XmlUtil.ReadResourceXmlString("achievementtaskinfo"));
    }

    [HttpPost]
    //[Produces("application/xml")]
    [Route("AchievementWebService.asmx/GetAllRewardTypeMultiplier")]
    public IActionResult GetAllRewardTypeMultiplier() {
        // TODO
        return Ok(XmlUtil.ReadResourceXmlString("rewardmultiplier"));
    }

    [HttpPost]
    [Produces("application/xml")]
    [Route("AchievementWebService.asmx/GetAchievementsByUserID")]
    public IActionResult GetAchievementsByUserID([FromForm] string userId) {
        // TODO: this is a placeholder
        ArrayOfUserAchievementInfo arrAchievements = new ArrayOfUserAchievementInfo {
            UserAchievementInfo = new UserAchievementInfo[]{
                new UserAchievementInfo {
                    UserID = Guid.Parse(userId),
                    AchievementPointTotal = 0,
                    RankID = 1,
                    PointTypeID = 1
                }
            }
        };

        return Ok(arrAchievements);
    }

    [HttpPost]
    [Produces("application/xml")]
    [Route("AchievementWebService.asmx/SetAchievementAndGetReward")]
    public IActionResult SetAchievementAndGetReward([FromForm] string apiToken, [FromForm] int achievementID) {
        // TODO: This is a placeholder; returns 5 gems
        Viking? viking = ctx.Sessions.FirstOrDefault(x => x.ApiToken == apiToken).Viking;
        return Ok(new AchievementReward[1] {
            new AchievementReward {
                Amount = 5,
                PointTypeID = 5,
                EntityID = Guid.Parse(viking.Id),
                EntityTypeID = 1,
                RewardID = 552
            }
        });
    }

        [HttpPost]
    [Produces("application/xml")]
    [Route("V2/AchievementWebService.asmx/SetUserAchievementTask")]
    [DecryptRequest("achievementTaskSetRequest")]
    public IActionResult SetUserAchievementTask([FromForm] string apiToken, [FromForm] int achievementID) {
        // TODO: This is a placeholder
        string xml = Request.Form["achievementTaskSetRequest"];
        AchievementTaskSetResponse response = new AchievementTaskSetResponse {
            Success = true,
            UserMessage = true,
            AchievementName = "Placeholder Achievement",
            Level = 1,
            AchievementTaskGroupID = 1279,
            LastLevelCompleted = true,
            AchievementInfoID = 1279,
            AchievementRewards = new AchievementReward[1] {
                new AchievementReward {
                    Amount = 25,
                    PointTypeID = 1,
                    RewardID = 910,
                    EntityTypeID =1
                }
            }
        };
        return Ok(new ArrayOfAchievementTaskSetResponse { AchievementTaskSetResponse = new AchievementTaskSetResponse[1] { response } });
    }
}
