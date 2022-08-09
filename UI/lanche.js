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
                    <div class="d-flex flex-row bd-highlight mb-3">
                        <div class="p-2 w-50 bd-highlight">    
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
                                    <select class="form-select" v-model="nomecategoria">
                                        <option v-for="c in categorias">
                                        {{c.nomecategoria}}
                                        
                                        </option>
                                    </select>
                                </div>
                                <p>{{idcategoria}}</p>
                            </div>
                           
                            <div class="p-2 w-50 bd-highlight">
                                <img width="250" height="250"
                                :src="photoPath+imagemlanche"/>
                                <input class="m-2" type="file" @change="imageUpload" />
                            </div>
                        </div>

                        <button type="button" @click="exibirID()" v-if="idlanche==0" class="btn btn-primary">Salvar</button>
                        <button type="button" @click="exibirID()" v-if="idlanche!=0" class="btn btn-primary">Alterar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
`,
data(){
    return{
        lanches:[],
        categorias:[],
        nomecategoria:"",
        idcategoria:0,
        modalTitle:"",
        idlanche:0,
        nomelanche:"",
        valorlanche:0,
        descricaolanche:"",
        imagemlanche:"",
        imagemlanche:"anonimo.jpg",
        photoPath:variaveis.PHOT_URL
    }
},
methods:{
    refreshData(){
        axios.get(variaveis.API_URL+"lanche").then((response)=>{
            this.lanches=response.data;
        });
        axios.get(variaveis.API_URL+"categoria").then((response)=>{
            this.categorias=response.data;
        });
        
    },
    exibirID(){
        alert(this.idcategoria);
    },
    addClick(){
        this.modalTitle="Adicionar lanche";
        this.idlanche=0;
        this.nomelanche="";
        this.valorlanche=0;
        this.descricaolanche="";
        this.imagemlanche="anonimo.jpg";
        this.idcategoria=0;
    },
    editClick(l){
        this.modalTitle="Editar lanche";
        this.idlanche=l.idlanche;
        this.nomelanche=l.nomelanche;
        this.valorlanche=l.valorlanche;
        this.descricaolanche=l.descricaolanche;
        this.imagemlanche=l.imagemlanche;
        this.nomecategoria=l.nomecategoria;
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
            idcategoria:this.nomecategoria
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
    },
    imageUpload(event){
        let formData = new FormData();
        formData.append('file',event.target.files[0]);
        axios.post(
            variaveis.API_URL+"lanche/savefile",formData).then((response)=>{
                this.imagemlanche=response.data;
            });
        
    }
},
mounted:function(){
    this.refreshData();
}
}