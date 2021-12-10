<template>
  <v-card
    class="mx-auto"
    max-width="400"
  >
    <v-img
      class="white--text align-end"
      height="200px"
      :src="pet.petImage"
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
        @click="deletePet"
      >
        Delete
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
</style>