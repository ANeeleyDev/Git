<template>
  <v-card
    class="mx-auto"
    max-width="400"
  >
    <v-img
      class="white--text align-end"
      height="200px"
      src="http://cdn.akc.org/content/article-body-image/lab_puppy_dog_pictures.jpg"
    >
      <v-card-title>{{pet.petName}}</v-card-title>
    </v-img>

    <v-card-subtitle class="pb-0">
      Age {{pet.age}}
    </v-card-subtitle>

    <v-card-text class="text--primary">
      <div>{{pet.otherComments}}</div>
      
    </v-card-text>

    <v-card-actions>
      <v-btn
        color="orange"
        text
      >
        Share
      </v-btn>

      <v-btn
        color="orange"
        text
      >
        Explore
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "pet-detail",
  props: ["pet"],
  calculated: {
      getRandomPic(){
          
          petService.getRandomPic().then((response) => {
            if (response.status === 200) {
              return response.data.message
            }
          })
      }
  }
};
</script>

<style>
</style>