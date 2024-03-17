const levelQuest = {
    positionX : 4500,
    idleMessage : '<p>This is wrong .. <br> people are becoming zombies.. <br> <span>Talk (Enter)</span></p>',
    quest : () => {
        const message = {
            start : 'There are monsters popped up, infecting people to become zombies.. kill the zombies to save people, and <span>level up to 5</span> to prove your strength. then I will help you beat the Zombie king.</p>',
            ing : 'You need to level up more',
            suc : 'You made it ! I will make you more stronger',
            end : 'Thank you ! Wish you good luck'
        }

        let messageState = '';

        if(!npcOne.questStart){
            messageState = message.start;
            npcOne.questStart = true;
        }else if (npcOne.questStart && !npcOne.questEnd && hero.level < 5){
            messageState = message.ing;
        }else if (npcOne.questStart && !npcOne.questEnd && hero.level >= 5){
            messageState = message.suc;
            npcOne.questEnd = true;
            hero.heroUpgrade(50000);
        }else if (npcOne.questStart && npcOne.questEnd){
            messageState = message.end;
        }
        

        let text = '';
        text += '<figure class="npc_img">'
        text += '<img src="./../lib/images/npc.png">'
        text += '</figure>'
        text += '<p>'
        text += messageState;
        text += '</p>'
        const modalInner = document.querySelector('.quest_modal .inner_box .quest_talk');
        modalInner.innerHTML = text;
    }
}

const levelQuestTwo = {
    positionX : 8500,
    idleMessage : '<p>Zombie king is about to resurrect.. <br> people are becoming zombies.. <br> <span>Talk (Enter)</span></p>',
    quest : () => {
        const level = 7;
        const message = {
            start : `There are monsters popped up, infecting people to become zombies.. kill the zombies to save people, and <span>level up to ${level}</span> to prove your strength. then I will help you beat the Zombie king.</p>`,
            ing : 'You need to level up more',
            suc : 'You made it ! I will make you more stronger',
            end : 'Thank you ! Wish you good luck'
        }

        let messageState = '';

        if(!npcTwo.questStart){
            messageState = message.start;
            npcTwo.questStart = true;
        }else if (npcTwo.questStart && !npcTwo.questEnd && hero.level < level){
            messageState = message.ing;
        }else if (npcTwo.questStart && !npcTwo.questEnd && hero.level >= level){
            messageState = message.suc;
            npcTwo.questEnd = true;
            hero.heroUpgrade(70000);
        }else if (npcTwo.questStart && npcTwo.questEnd){
            messageState = message.end;
        }
        

        let text = '';
        text += '<figure class="npc_img">'
        text += '<img src="./../lib/images/npc.png">'
        text += '</figure>'
        text += '<p>'
        text += messageState;
        text += '</p>'
        const modalInner = document.querySelector('.quest_modal .inner_box .quest_talk');
        modalInner.innerHTML = text;
    }
}