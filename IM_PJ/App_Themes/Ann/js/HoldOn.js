
(function(window){'use strict';
    
    function HoldOnAction(){
            if("undefined"==typeof jQuery){
                throw new Error("HoldOn.js requires jQuery");
            }
            
            var HoldOn = {};
            
            HoldOn.open = function(properties){
                $('#holdon-overlay').remove();//RemoveIfCalledBefore
                var theme = "sk-rect";
                var content = "";

                var myArray = [
                    "Hãy hát như chẳng có ai nghe... Hãy nhảy múa như không có ai nhìn...", 
                    "Ai làm bằng gió thì sẽ được trả lương bằng khói.",
                    "Đường tuy ngắn, không đi không đến. Việc tuy nhỏ, không làm không nên.",
                    "Không có áp lực, không có kim cương.",
                    "Đừng khoe sự khởi đầu của công việc, mà hãy khoe khi bạn đã làm xong công việc.",
                    "Khi bạn yêu thích công việc của mình, ngày nào cũng là ngày lễ.",
                    "Sai một lần thì có thể cho là tai nạn. Nhưng lặp đi lặp lại lỗi sai đó thì chính là sự lựa chọn.",
                    "Hạnh phúc là khi ta có 1 việc để làm, 1 người để yêu và 1 điều để hy vọng ^_^",
                    "Không phải lúc nào bạn cố gắng cũng thành công, nhưng phải luôn cố gắng để không hối tiếc khi thất bại.",
                    "Hãy làm những việc bình thường bằng lòng say mê phi thường, thành công sẽ đến với bạn.",
                    "Trí tuệ của con người trưởng thành trong tĩnh lặng, còn tính cách trưởng thành trong bão táp.",
                    "Đừng để đến ngày mai những việc gì anh có thể làm hôm nay.",
                    "Thành công chỉ đến khi bạn làm việc tận tâm và luôn nghĩ đến những điều tốt đẹp.",
                    "Không có nghèo gì bằng không có tài, không có gì hèn bằng không có chí.",
                    "Hãy làm tròn mỗi công việc của đời mình như thể đó là công việc cuối cùng.",
                    "Đường đi khó không phải vì ngăn sông cách núi. Mà khó vì lòng người ngại núi e sông.",
                    "Ai than thở không bao giờ có thời gian, người ấy làm được ít việc nhất.",
                    "Làm việc đừng quá trông đợi vào kết quả, nhưng hãy mong cho mình làm được hết sức mình.",
                    "Chiến thắng bản thân là chiến thắng hiển hách nhất.",
                    "Bí quyết lớn nhất của thành công là thành thật. Không thành thật, không có phương pháp nào đắc dụng với bạn hết.",
                    "Đường tuy gần không đi không bao giờ đến, việc tuy nhỏ không làm chẳng bao giờ nên.",
                    "Bạn càng thành công thì ở gần bạn càng ít người vui vì sự thành công của bạn.",
                    "Mọi công việc thành đạt đều nhờ sự kiên trì và lòng say mê.",
                    "Thành công không phụ thuộc vào kiến thức mà vào cách áp dụng kiến thức.",
                    "Nếu thời gian là thứ đáng giá nhất, phí phạm thời gian hẳn phải là sự lãng phí ngông cuồng nhất.",
                    "Quan trọng không phải là sống lâu như thế nào, mà là sống sâu như thế nào.",
                    "Đam mê tái tạo thế giới cho tuổi trẻ. Nó khiến mọi thứ trở nên sống động và đáng kể.",
                    "Thật dễ nuối tiếc về một điều gì đó đã mất đi, nhưng sẽ rất khó nhận ra và trân trọng những gì ta đang có…",
                    "Người ta có thể quên đi điều bạn nói, nhưng những gì bạn để lại trong lòng họ thì không bao giờ phai nhạt",
                    "Ai cũng có lòng tự trọng, tự tin. Không có lòng tự trọng, tự tin là người vô dụng.",
                    "Ngay cả khi trong túi hết tiền, cái mũ trên đầu anh cũng phải đội cho ngay ngắn.",
                    "Niềm tin là một thứ khó tìm nhất nhưng cũng là thứ dễ mất nhất.",
                    "Lòng tin giống như một cục tẩy, nó sẽ mòn dần sau mỗi lỗi sai mà ta mắc phải.",
                    "Ở đời, sống mà cứ ngại ngùng cả nể mãi, thì chẳng thể nào tiến xa được. Nhiều lúc cứ phải mặt dày lên mà sống.",
                    "Khi ta còn thở, ta còn hy vọng",
                    "Chính trong lao động và chỉ có lao động, con người mới trở nên vĩ đại và có niềm tin trọn vẹn",
                    "Cư xử với con người bằng sự chân thành sẽ được đáp lại bằng sự chân thành.",
                    "Ai chiến thắng không hề chiến bại? Ai nên khôn chẳng dại đôi lần?",
                    "Bạn có yêu đời không? Vậy đừng phung phí thời gian vì chất liệu của cuộc sống được làm bằng thời gian.",
                    "Khi tất cả những cái khác đã mất đi thì tương lai vẫn còn!",
                    "Đừng đi qua thời gian mà không để lại dấu vết",
                    "Sự chia sẻ và tình yêu thương là điều quý giá nhất trên đời...",
                    "Nếu không biết đau thì không thể đứng lên khi gục ngã",
                    "Có 3 cách để tự làm giàu cho mình: Mỉm cười, Cho đi, và Tha thứ.",
                    "Tài năng thường được tỏa sáng trong sự im lặng, còn cái kém cỏi thì thường tự lan tỏa bằng âm thanh.",
                    "Một nụ cười có thể thay đổi 1 ngày. Một cái ôm có thể thay đổi 1 tuần. Một lời nói có thể thay đổi 1 cuộc sống."
                    ];

                var message = myArray[Math.floor(Math.random() * myArray.length)];
                
                if(properties){
                    if(properties.hasOwnProperty("theme")){//Choose theme if given
                        theme = properties.theme;
                    }
                    
                    if(properties.hasOwnProperty("message")){//Choose theme if given
                        message = properties.message;
                    }
                }
                
                switch(theme){
                    case "custom":
                        content = '<div style="text-align: center;">' + properties.content + "</div>";
                    break;
                    case "sk-dot":
                        content = '<div class="sk-dot"> <div class="sk-dot1"></div> <div class="sk-dot2"></div> </div>';
                    break;
                    case "sk-rect":
                        content = '<div class="sk-rect"> <div class="rect1"></div> <div class="rect2"></div> <div class="rect3"></div> <div class="rect4"></div> <div class="rect5"></div> </div>';
                    break;
                    case "sk-cube":
                        content = '<div class="sk-cube"> <div class="sk-cube1"></div> <div class="sk-cube2"></div> </div>';
                    break;
                    case "sk-bounce":
                        content = '<div class="sk-bounce"> <div class="bounce1"></div> <div class="bounce2"></div> <div class="bounce3"></div> </div>';
                    break;
                    case "sk-circle":
                        content = '<div class="sk-circle"> <div class="sk-circle1 sk-child"></div> <div class="sk-circle2 sk-child"></div> <div class="sk-circle3 sk-child"></div> <div class="sk-circle4 sk-child"></div> <div class="sk-circle5 sk-child"></div> <div class="sk-circle6 sk-child"></div> <div class="sk-circle7 sk-child"></div> <div class="sk-circle8 sk-child"></div> <div class="sk-circle9 sk-child"></div> <div class="sk-circle10 sk-child"></div> <div class="sk-circle11 sk-child"></div> <div class="sk-circle12 sk-child"></div> </div>';
                    break;
                    case "sk-cube-grid":
                        content = '<div class="sk-cube-grid"> <div class="sk-cube-child sk-cube-grid1"></div> <div class="sk-cube-child sk-cube-grid2"></div> <div class="sk-cube-child sk-cube-grid3"></div> <div class="sk-cube-child sk-cube-grid4"></div> <div class="sk-cube-child sk-cube-grid5"></div> <div class="sk-cube-child sk-cube-grid6"></div> <div class="sk-cube-child sk-cube-grid7"></div> <div class="sk-cube-child sk-cube-grid8"></div> <div class="sk-cube-child sk-cube-grid9"></div> </div>';
                    break;
                    case "sk-folding-cube":
                        content = '<div class="sk-folding-cube"> <div class="sk-cubechild1 sk-cube-parent"></div> <div class="sk-cubechild2 sk-cube-parent"></div> <div class="sk-cubechild4 sk-cube-parent"></div> <div class="sk-cubechild3 sk-cube-parent"></div> </div>';
                    break;
                    case "sk-fading-circle":
                        content = '<div class="sk-fading-circle"> <div class="sk-fading-circle1 sk-circle-child"></div> <div class="sk-fading-circle2 sk-circle-child"></div> <div class="sk-fading-circle3 sk-circle-child"></div> <div class="sk-fading-circle4 sk-circle-child"></div> <div class="sk-fading-circle5 sk-circle-child"></div> <div class="sk-fading-circle6 sk-circle-child"></div> <div class="sk-fading-circle7 sk-circle-child"></div> <div class="sk-fading-circle8 sk-circle-child"></div> <div class="sk-fading-circle9 sk-circle-child"></div> <div class="sk-fading-circle10 sk-circle-child"></div> <div class="sk-fading-circle11 sk-circle-child"></div> <div class="sk-fading-circle12 sk-circle-child"></div> </div>';
                    break;
                    default:
                        content = '<div class="sk-rect"> <div class="rect1"></div> <div class="rect2"></div> <div class="rect3"></div> <div class="rect4"></div> <div class="rect5"></div> </div>';
                        console.warn(theme + " doesn't exist for HoldOn.js");
                    break;
                }
                
                var Holder    = '<div id="holdon-overlay" style="display: none;">\n\
                                    <div id="holdon-content-container">\n\
                                        <div id="holdon-content">'+content+'</div>\n\
                                        <div id="holdon-message">'+message+'</div>\n\
                                    </div>\n\
                                </div>';
                
                $(Holder).appendTo('body').fadeIn(300);
                
                if(properties){
                    if(properties.backgroundColor){
                        $("#holdon-overlay").css("backgroundColor",properties.backgroundColor);
                    }
                    
                    if(properties.backgroundColor){
                        $("#holdon-message").css("color",properties.textColor);
                    }
                }
            };
            
            HoldOn.close = function(){
                $('#holdon-overlay').fadeOut(300, function(){
                    $(this).remove();
                });
            };
            
        return HoldOn;
    }
    
    if(typeof(HoldOn) === 'undefined'){
        window.HoldOn = HoldOnAction();
    }
    
})(window);
