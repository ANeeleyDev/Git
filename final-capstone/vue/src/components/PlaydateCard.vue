<template>
 <v-card
    class="mx-auto"
    max-width="800"
    >
    <vtitle>{{playdate.meetingTime}}</vtitle>
    <v-card-text class="pd-comments">
        <div>{{playdate.playdateAddress}} {{playdate.playdateCity}}, {{playdate.playdateState}} {{playdate.playdateZip}}</div>
    </v-card-text>
    <!-- <v-img class="pd-pet-pic" height="200px"
        src={{pet.petImage}} >

    <v-card-title>
        Come Pay With Me!
    </v-card-title>

    <v-card-subtitle class="pd-name">
        Name {{pet.petname}}
    </v-card-subtitle>
    <v-card-subtitle class="pd-age">
        Age {{pet.age}}
    </v-card-subtitle>

    <v-card-subtitle class="pd-breed">
        Breed {{pet.breed}}
    </v-card-subtitle>

    <v-card-text class="pd-comments">
        <div>{{pet.otherComments}}</div>
    </v-card-text>

    <v-card-actions>
        <v-btn
            color="green"
            text
        >
        Request Playdate
        </v-btn>

        <v-btn
            color="blue"
            text
        >
        Save 
        </v-btn>
    </v-card-actions> -->
 </v-card>


</template>

<script>

export default {
    name:"playdate-card",
    props: ["playdate", "petId"],
    
}
</script>