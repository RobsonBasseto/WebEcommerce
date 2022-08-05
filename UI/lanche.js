const lanche={template:`
<div>
        <button type="button" class="btn btn-primary m-2 fload-end" data-bs-toggle="modal"
                data-bs-target="#exampleModal" @click="addClick()">Adicionar</button>
        <table class="table" style="color: white;">
            <thead class="thead-dark">
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Valor</th>
                    <th>Descição</th>
                    <th>Categoria</th>
                    <th>Imagem</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="l in lanches">
                    <td>{{l.idlanche}}</td>
                    <td>{{l.nomelanche}}</td>
                    <td>{{l.valorlanche}}</td>
                    <td>{{l.descricaolanche}}</td>
                    <td>{{l.nomecategoria}}</td>
                    <td>{{l.imagemlanche}}</td>
                    
                    <td>
                        <button type="button" class="btn btn-light mr-1" data-bs-toggle="modal"
                                data-bs-target="#exampleModal" @click="editClick(l)">editar</button>
                        <button type="button" class="btn btn-light mr-1" @click="deleteClick(l.idlanche)">deletar</button>
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
                            <span class="input-group-text">Nome</span>
                            <input type="text" class="form-control" v-model="nomelanche ">
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Valor</span>
                            <input type="text" class="form-control" v-model="valorlanche ">
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Descrição</span>
                            <input type="text" class="form-control" v-model="descricaolanche ">
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Categoria</span>
                            <input type="text" class="form-control" v-model="idcategoria">
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text">Imagem</span>
                            <input type="text" class="form-control" v-model="imagemlanche">
                        </div>
                        
                        <button type="button" @click="createClick()" v-if="idlanche==0" class="btn btn-primary">Salvar</button>
                        <button type="button" @click="updateClick()" v-if="idlanche!=0" class="btn btn-primary">Alterar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
`,
data(){
    return{
        lanches:[],
        modalTitle:"",
        idlanche:0,
        nomelanche:"",
        valorlanche:0,
        descricaolanche:"",
        imagemlanche:"",
        idcategoria:0
    }
},
methods:{
    refreshData(){
        axios.get(variaveis.API_URL+"lanche").then((response)=>{
            this.lanches=response.data;
        });
    },
    addClick(){
        this.modalTitle="Adicionar lanche";
        this.idlanche=0;
        this.nomelanche="";
        this.valorlanche=0;
        this.descricaolanche="";
        this.imagemlanche="";
        this.idcategoria=0;
    },
    editClick(l){
        this.modalTitle="Editar lanche";
        this.idlanche=l.idlanche;
        this.nomelanche=l.nomelanche;
        this.valorlanche=l.valorlanche;
        this.descricaolanche=l.descricaolanche;
        this.imagemlanche=l.imagemlanche;
        this.idcategoria=l.idcategoria;
    },
    createClick(){
        axios.post(variaveis.API_URL+"lanche",{
            nomelanche:this.nomelanche,
            valorlanche:this.valorlanche,
            descricaolanche:this.descricaolanche,
            imagemlanche:this.imagemlanche,
            idcategoria:this.idcategoria
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    updateClick(){
        axios.put(variaveis.API_URL+"lanche",{
            idlanche:this.idlanche,
            nomelanche:this.nomelanche,
            valorlanche:this.valorlanche,
            descricaolanche:this.descricaolanche,
            imagemlanche:this.imagemlanche,
            idcategoria:this.idcategoria
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
            
        });
    },
    deleteClick(id){
        if(!confirm("Tem certeza que deseja deletar?")){
            return;
        }
        axios.delete(variaveis.API_URL+"lanche/"+id                
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