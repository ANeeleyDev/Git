<template>
  <v-card class="mx-auto" max-width="400">
    <v-img class="white--text align-end" height="200px" :src="pet.petImage">
      <v-card-title>{{ pet.petName }}</v-card-title>
    </v-img>

    <v-card-subtitle class="pb-0"> Age: {{ pet.age }} </v-card-subtitle>

    <v-card-text class="text--primary">
      <div>{{ pet.otherComments }}</div>
    </v-card-text>

    <v-card-actions>
      <v-btn
        text
        color="orange accent-4"
        @click="reveal = !reveal"
      >
        Learn More
      </v-btn> 
      <v-btn color="orange" text v-if="this.pet.userId === this.$store.state.user.userId" @click="$router.push({name: 'edit-pet', params: {petId: pet.petId}})"> Edit </v-btn>
      <v-btn color="orange" text v-if="this.pet.userId === this.$store.state.user.userId" @click="deletePet"> Delete </v-btn>
     
      
    </v-card-actions>

    <v-expand-transition>
      <v-card
        v-if="reveal"
        class="transition-fast-in-fast-out v-card--reveal"
        style="height: 100%;"
      >
        <v-card-text class="pb-0">
          <p class="text-h4 text--primary">
            {{ pet.petName }}
          </p>
            
          <p> {{pet.species}}, {{pet.breed}}, {{pet.age}} years old</p>
          <p style="display:inline" >Attributes: </p>
          <p style="display:inline" v-if="pet.playful === true" >Playful</p>
          <p style="display:inline" v-if="pet.nervous === true & pet.playful === true">, </p>
          <p style="display:inline" v-if="pet.nervous === true" >Nervous</p>
          <p style="display:inline" v-if="pet.confident === true">, </p>
          <p style="display:inline" v-if="pet.confident === true" >Confident</p>
          <p style="display:inline" v-if="pet.shy === true">, </p>
          <p style="display:inline" v-if="pet.shy === true" >Shy</p>
          <p style="display:inline" v-if="pet.mischievous === true">, </p>
          <p style="display:inline" v-if="pet.mischievous === true" >Mischievous</p>
          <p style="display:inline" v-if="pet.independent === true">, </p>
          <p style="display:inline" v-if="pet.independent === true" >Independent</p>
          <p></p>
          <p>Other comments: "{{ pet.otherComments }}"</p>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-btn
            text
            color="teal accent-4"
            @click="reveal = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-expand-transition>
  </v-card>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "pet-detail",
  props: ["pet"],
  data(){
    return {
      reveal: false,
    }

  },
  methods: {
    deletePet() {
      if (
        confirm(
          "Are you sure you want to delete this pet? This action cannot be undone."
        )
      ) {
        petService
          .deletePet(this.pet.petId)
          .then((response) => {
            if (response.status === 200) {
              alert("Pet successfully deleted");
              this.$router.push(`/`);
            }
          })
          .catch((error) => {
            if (error.response) {
              this.errorMsg =
                "Error deleting pet. Response received was '" +
                error.response.statusText +
                "'.";
            } else if (error.request) {
              this.errorMsg =
                "Error deleting pet. Server could not be reached.";
            } else {
              this.errorMsg =
                "Error deleting pet. Request could not be created.";
            }
          });
      }
    },    
  },
};
</script>

<style>

.v-card--reveal {
  bottom: 0;
  opacity: 1 !important;
  position: absolute;
  width: 100%;
}

</style>