import axios from 'axios';


export default {

    //return all pets for user

    
    getPetList(userId) {
        return axios.get(`/pets/user/${userId}`)

    },

//return specific pet
    getPet(petId){
        return axios.get(`/pets/${petId}`)
    },

//register new pet
    addPet(petToSave) {
        console.log(petToSave);
        return axios.post(`/pets/register`, petToSave)
        
    },

    //edit pet detail

    // updatePet(petToEdit){
    //     return axios.put(``, petToEdit)
    // }

    //delete pet
    deletePet(petId) {
        return axios.delete(`/pets/${petId}`)
    }
}