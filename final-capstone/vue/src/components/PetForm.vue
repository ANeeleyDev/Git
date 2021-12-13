<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-text-field
        value="petName"
        label="Name"
        v-model="pet.petName"
      ></v-text-field>
       <v-text-field
        value="petImage"
        label="Pet Image URL"        
        v-model.number="pet.petImage"
      ></v-text-field>
      <v-text-field
        value="age"
        label="Age"        
        v-model.number="pet.age"
      ></v-text-field>
      <v-select
        :items="species"
        label="Species"
        v-model="pet.species"
      ></v-select>
      <v-select :items="breed" label="Breed" v-model="pet.breed"></v-select>
      <v-textarea
        name="input-7-1"
        label="Other Comments"
        value="What else should we know?."
        v-model="pet.otherComments"
      ></v-textarea>

      <v-checkbox
        label="Playful"        
        v-model="pet.playful"
      ></v-checkbox>
      <v-checkbox
        v-model="pet.mischievous"
        label="Mischievous"
        
      ></v-checkbox>
      <v-checkbox v-model="pet.shy" label="Shy"></v-checkbox>
      <v-checkbox v-model="pet.nervous" label="Nervous"></v-checkbox>
      <v-checkbox v-model="pet.confident" label="Confident"></v-checkbox>
      <v-checkbox v-model="pet.independent" label="Independent"></v-checkbox>

      <v-btn type="submit"> Submit </v-btn>
      <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </v-btn>
    </v-form>    
    
  </div>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "pet-form",
  props: ["petId"],

  data() {
    return {
      species: [
        {
          text: "Dog",
          value: "0",
        },
        {
          text: "Cat",
          value: "1",
        },
      ],
      breed: [
        {
          text: "Holder",
          value: "0",
        },
      ],
      pet: {
        petId: "",
        petName: "",
        petImage: "",
        age: "",
        species: "",
        breed: "",
        otherComments: "",
        playful: false,
        nervous: false,
        confident: false,
        shy: false,
        mischievous: false,
        independent: false,
      },
      editPetId: this.$route.params.petId,
    };
  },
  created() {
    if (this.$route.params.petId != undefined) {
      petService
        .getPet(this.$route.params.petId)
        .then((response) => {
          this.pet = response.data;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Pet not available. This pet may have been deleted or you have entered an invalid pet ID."
            );
            this.$router.push("/");
          }
        });
    }
  },
  methods: {
    submitForm() {
      const newPet = {
        userId: Number(this.$store.state.user.userId),
        petName: this.pet.petName,
        petImage: this.pet.petImage,
        age: this.pet.age,
        species: this.pet.species,
        breed: this.pet.breed,
        otherComments: this.pet.otherComments,
        playful: this.pet.playful,
        nervous: this.pet.nervous,
        confident: this.pet.confident,
        shy: this.pet.shy,
        mischievous: this.pet.mischievous,
        independent: this.pet.independent,
      };

      if (this.editPetId === undefined) {
        //add here
        petService
          .addPet(newPet)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/petlist`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update else

        newPet.petId = this.editPetId;

        petService

          .updatePet(this.editPetId, newPet)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/petView`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    cancelForm() {
      this.$router.push(`/petlist`);
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " pet. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg = "Error " + verb + " pet. Server could not be reached.";
      } else {
        this.errorMsg = "Error " + verb + " pet. Request could not be created.";
      }
    },
  },
};
</script>

<style>
</style>