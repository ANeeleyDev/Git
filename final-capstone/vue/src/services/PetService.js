import axios from 'axios';


export default {
    

    //return all pets for user

    
    getPetList() {
        return axios.get(`/pets/mypets`)

    },

//return specific pet
    getPet(petId){
        return axios.get(`/pets/${petId}`)
    },

//register new pet
    addPet(petToSave) {
        console.log(petToSave);
        return axios.post(`/pets/mypets/register`, petToSave)
        
    },

    //edit pet detail

    updatePet(petId, petToEdit){
        return axios.put(`/pets/mypets/${petId}`, petToEdit)
    },

    //delete pet
    deletePet(petId) {
        return axios.delete(`/pets/mypets/${petId}`)
    },

    //show five most recently added pets

    getTopFivePets() {
        return axios.get(`/pets/recent`)
    },

}