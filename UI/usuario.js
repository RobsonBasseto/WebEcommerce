const usuario={template:`
<div id="container-cadastro">
        <h3>Cadastro</h3>
        <div class="form-group">
            <div class="row">
                <div class="col-md-9">
                    <label >Nome Completo:</label>
                    <input type="text" class="form-control" v-model="nome"/>
                </div>
                <div class="col-md-9">
                    <label >E-mail:</label>
                    <input type="text" class="form-control" v-model="email"/>
                </div>
                <div class="col-md-9">
                    <label >Senha:</label>
                    <input type="password" class="form-control" v-model="senha"/>
                </div>
                <div class="col-md-4">
                    <label >CEP:</label>
                    <input type="text" class="form-control" v-model="cep"/>
                </div>
                <div class="col-md-5">
                    <label >Cidade:</label>
                    <input type="text" class="form-control" v-model="cidade"/>
                </div>
                <div class="col-md-9">
                    <label >Bairro:</label>
                    <input type="text" class="form-control" v-model="bairro"/>
                </div>
                <div class="col-md-6">
                    <label >Rua:</label>
                    <input type="text" class="form-control" v-model="rua"/>
                </div>
                <div class="col-md-3">
                    <label >Número:</label>
                    <input type="text" class="form-control" v-model="numero"/>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-6">
                
                    <button type="button" @click="createClick()" class="btn btn-light btn-outline-primary" id="btn-Criar">Criar</button>
                </div>
            </div>
        </div>
</div>
`,
data(){
    return{
        idusuario:0,
        nome:"",
        email:"",
        senha:"",
        cep:"",
        cidade:"",
        rua:"",
        bairro:"",
        numero:0
    }
},
methods:{
    createClick(){
        axios.post(variaveis.API_URL+"usuario",{
            nome:this.nome,
            email:this.email,
            senha:this.senha,
            cep:this.cep,
            cidade:this.cidade,
            rua:this.rua,
            bairro:this.bairro,
            numero:this.numero
        }).then((response)=>{
            alert(response.data);
            this.$router.push('/login')
        });
        
    }
}
}