mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.log("Hello");
  },

  GetPlayerData: function () {
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  }, 

  RateGame: function () {
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          console.log(feedbackSent);
        })
      } else {
        console.log(reason)
      }
    })
  },

  AuthPlayer: function () {
    auth();
  }, 

  SetLeaderboardScore: function (value) {
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('Score', value);
    });
  }, 

  ShowAdv: function (){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
  }, 

});