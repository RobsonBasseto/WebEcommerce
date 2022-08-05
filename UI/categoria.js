const categoria={template:`
<div>
        <button type="button" class="btn btn-primary m-2 fload-end" data-bs-toggle="modal"
                data-bs-target="#exampleModal" @click="addClick()">Adicionar</button>
        <table class="table" style="color: white;">
            <thead class="thead-dark">
                <tr>
                    <th> ID Categoria</th>
                    <th>Nome Categoria</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="c in categorias">
                    <td>{{c.idcategoria}}</td>
                    <td>{{c.nomecategoria}}</td>
                    <td>
                        <button type="button" class="btn btn-light mr-1" data-bs-toggle="modal"
                                data-bs-target="#exampleModal" @click="editClick(c)">editar</button>
                        <button type="button" class="btn btn-light mr-1" @click="deleteClick(c.idcategoria)">deletar</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="modal fade" id="exampleModal" tabindex="-1">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLabel" style="color: black;">{{modalTitle}}</h5>
                        <button type="button" class="bnt-close" data-bs-dismiss="modal" aria-label="Close">X
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <span class="input-group-text">Nome da categoria</span>
                            <input type="text" class="form-control" v-model="nomecategoria">
                        </div>
                        <button type="button" @click="createClick()" v-if="idcategoria==0" class="btn btn-primary">Salvar</button>
                        <button type="button" @click="updateClick()" v-if="idcategoria!=0" class="btn btn-primary">Alterar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
`,
data(){
    return{
        categorias:[],
        modalTitle:"",
        idcategoria:0,
        nomecategoria:""
    }
},
methods:{
    refreshData(){
        axios.get(variaveis.API_URL+"categoria").then((response)=>{
            this.categorias=response.data;
        });
    },
    addClick(){
        this.modalTitle="Adicionar categoria";
        this.idcategoria=0;
        this.nomecategoria="";
    },
    editClick(c){
        this.modalTitle="Editar categoria";
        this.idcategoria= c.idcategoria;
        this.nomecategoria= c.nomecategoria;
    },
    createClick(){
        axios.post(variaveis.API_URL+"categoria",{
            nomecategoria:this.nomecategoria
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    updateClick(){
        axios.put(variaveis.API_URL+"categoria",{
            idcategoria:this.idcategoria,
            nomecategoria:this.nomecategoria
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
            
        });
    },
    deleteClick(id){
        if(!confirm("Tem certeza que deseja deletar?")){
            return;
        }
        axios.delete(variaveis.API_URL+"categoria/"+id                
        ).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    }
},
mounted:function(){
    this.refreshData();
}
}